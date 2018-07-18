using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diseniandote.Models;
using PayPal.Api;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Diseniandote.Controllers
{
    public class PayPalController : Controller
    {
		DiseniandoteEntities db = new DiseniandoteEntities();
		// GET: PayPal
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult RealzarPedido()
		{
			if (Session["carrito"] != null)
			{
				List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
				
				//Instancia de la tabla Pedido
				Pedido nuevoPedido = new Pedido();
				
				//Recolectar datos para pedido
				nuevoPedido.fechaEntrega = DateTime.Now;
				nuevoPedido.estatus = true;
				nuevoPedido.total = (double)compras.Sum(x => x.Cantidad * x.Producto.precioVenta);
				nuevoPedido.total = (nuevoPedido.total * 0.16) + nuevoPedido.total;
				nuevoPedido.idUsuario = Int32.Parse(User.Identity.GetUserId());
				//recolectar datos para detalle pedido
				nuevoPedido.DetallePedido = (from item in compras
											 select new DetallePedido
											 {
												 cantidad = item.Cantidad,
												 nombrePoducto = item.Producto.nombre,
												 subtotal = (double)item.Producto.precioVenta * item.Cantidad,
												 precioDesc = 0,
												 idProducto = item.Producto.idProducto
											 }).ToList();
											 
				//Insersion en tabla pedido y detalle pedido
				db.Pedido.Add(nuevoPedido);
				try
				{
					//Guardar cambios
					db.SaveChanges();
				}
				catch (DbEntityValidationException ex)
				{
					// Retrieve the error messages as a list of strings.
					var errorMessages = ex.EntityValidationErrors
							.SelectMany(x => x.ValidationErrors)
							.Select(x => x.ErrorMessage);

					// Join the list to a single string.
					var fullErrorMessage = string.Join("; ", errorMessages);

					// Combine the original exception message with the new one.
					var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

					// Throw a new DbEntityValidationException with the improved exception message.
					throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
				}
			}
			return View("Success");
		}

		//trabajar con paypal payment
		private Payment payment;

		//crear a paypament usando un APIContext
		private Payment CreatePayment(APIContext apiContext, string redirectUrl)
		{
			var listItems = new ItemList() { items = new List<Item>() };
			List<CarritoItem> lstCompras = (List<CarritoItem>)Session["carrito"];
			foreach (var car in lstCompras)
			{
				listItems.items.Add(new Item()
				{
					name = car.Producto.nombre,
					currency = "USD",
					price = car.Producto.precioVenta.ToString(),
					quantity = car.Cantidad.ToString(),
					sku = "sku"
				});
			}

			var payer = new Payer() { payment_method = "paypal" };

			//Hacer la configuracion RedirectURLs aqui con redirectURLs objeto
			var rediUrls = new RedirectUrls()
			{
				cancel_url = redirectUrl,
				return_url = redirectUrl
			};

			//Crear objeto de detalle
			var details = new Details()
			{
				tax = "1",
				shipping = "2",
				subtotal = lstCompras.Sum(x => x.Cantidad * x.Producto.precioVenta).ToString()
			};

			//Crear amount objeto
			var amount = new Amount()
			{
				currency = "USD",
				total = (Convert.ToDouble(details.tax + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal))).ToString(), //tax + shipping + subtotal
				details = details
			};

			//crear transaccion
			var transactioList = new List<Transaction>();
			transactioList.Add(new Transaction()
			{
				description = "Chien Testing transaction description",
				invoice_number = Convert.ToString((new Random()).Next(100000)),
				amount = amount,
				item_list = listItems
			});

			payment = new Payment()
			{
				intent = "sale",
				payer = payer,
				transactions = transactioList,
				redirect_urls = rediUrls
			};

			return payment.Create(apiContext);
		}

		//crear ExecutePayment  metodo
		private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
		{
			var paymentExecution = new PaymentExecution()
			{
				payer_id = payerId
			};
			payment = new Payment() { id = paymentId };
			return payment.Execute(apiContext, paymentExecution);
		}

		//crear PaymentWithPaypal method
		public ActionResult PaymentWithPaypal()
		{
			// Conseguir context de el paypal en clienteid y clientsecret
			APIContext apiContext = PayConfiguracion.GetAPIContext();
			try
			{
				string payerId = Request.Params["PayerID"];
				if (string.IsNullOrEmpty(payerId))
				{
					//crear un payment
					string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PayPal/PaymentWithPaypal?";
					var guid = Convert.ToString((new Random()).Next(100000));
					var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

					//conseguir links regresados de paypal response para crear call function
					var links = createdPayment.links.GetEnumerator();
					string paypalRedirecUrl = string.Empty;

					while (links.MoveNext())
					{
						Links link = links.Current;
						if (link.rel.ToLower().Trim().Equals("approval_urel"))
						{
							paypalRedirecUrl = link.href;
						}
					}
					Session.Add(guid, createdPayment.id);
					return Redirect(paypalRedirecUrl);
				}
				else
				{
					//Aqui sera ejecutado cuando nosotros recibamos todo los parametros payment de la previa llamada
					var guid = Request.Params["guid"];
					var executePayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
					if (executePayment.state.ToLower() != "approved")
					{
						return View("Failure");
					}
				}
			}
			catch (Exception ex)
			{

				PayPalLogger.Log("Error: " + ex.Message);
				return View("Failure");
			}
			return View("Success");
			//Session.Remove(carrito);
		}
	}
}
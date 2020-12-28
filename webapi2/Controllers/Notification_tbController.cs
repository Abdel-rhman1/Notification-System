using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using webapi2;
using System.Net.Http.Handlers;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace webapi2.Controllers
{
    [RoutePrefix("api/Noti")]
    public class Notification_tbController : ApiController
    {
        private Notification_SystemEntities3 db = new Notification_SystemEntities3();
        [HttpGet]
        //[Route ("find/{id}")]
        public HttpResponseMessage Find(int id)
        {
            try
            {
                var res = new HttpResponseMessage(HttpStatusCode.OK);
                res.Content = new StringContent(JsonConvert.SerializeObject(
                    db.Notification_tb.Single(p => p.id == id).content));
                res.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return res;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
        [HttpGet]
        public HttpResponseMessage Findall()
        {
            try
            {
                var res = new HttpResponseMessage(HttpStatusCode.OK);
                res.Content = new StringContent(JsonConvert.SerializeObject(
                    db.Notification_tb.ToList()));
                res.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return res;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        /**
         * DELETE : api/Notification_tb/3 
        */
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var res = new HttpResponseMessage(HttpStatusCode.OK);
                db.Notification_tb.Remove(db.Notification_tb.Single(p => p.id == id));
                db.SaveChanges();
                return res;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage create(Notification_tb nottif)
        {
            try
            {
                var res = new HttpResponseMessage(HttpStatusCode.OK);
                db.Notification_tb.Add(nottif);
                db.SaveChanges();
                return res;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
        
        [HttpPut]
        public HttpResponseMessage Update(Notification_tb notifi)
        {
            try
            {
                var res = new HttpResponseMessage(HttpStatusCode.OK);
                var newNotification = db.Notification_tb.Single(p => p.id == notifi.id);
                newNotification.id = notifi.id;
                newNotification.subjec = notifi.subjec;
                newNotification.content = notifi.content;
                newNotification.lang = notifi.lang;
                db.SaveChanges();
                return res;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
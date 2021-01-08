using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
namespace webapi2
{
    public static class WebApiConfig{
        public static void Register(HttpConfiguration config){
            int bitwise = 3;
            Notification_SystemEntities4 db = new Notification_SystemEntities4();
            if (bitwise == 0){
                var smsCount = db.mailQueues;
                int id = 0;
                foreach (var item in smsCount){
                    id = item.id;
                    if (item.stat == 0) continue;
                    else break;
                }
                var Notification = db.mailQueues.Single(p => p.id == id);
                Debug.WriteLine(Notification.toUser);
                Debug.WriteLine(Notification.content);
                Debug.WriteLine("sending at : "+DateTime.Now);
                db.mailQueues.Remove(db.mailQueues.Single(p => p.id == id));
                db.SaveChanges();
            }else if(bitwise==1){
                var smsCount = db.smsQueues;
                int id = 0;
                foreach (var item in smsCount)
                {
                    id = item.id;
                    if (item.stat == 0) continue;
                    else break;
                }
                var Notification = db.smsQueues.Single(p => p.id == id);
                Debug.WriteLine(Notification.toUser);
                Debug.WriteLine(Notification.content);
                Debug.WriteLine("sending at : " + DateTime.Now);
                db.smsQueues.Remove(db.smsQueues.Single(p => p.id == id));
                db.SaveChanges();
            }
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
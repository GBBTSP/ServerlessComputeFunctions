#r "Newtonsoft.Json"

using System;
using System.Net;
using Newtonsoft.Json;
using System.Net.Mail;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"order recieved to validate");
    MailAddress addr;
    Order order;
    
    try{
        string jsonContent = await req.Content.ReadAsStringAsync();
        order = JsonConvert.DeserializeObject<Order>(jsonContent);
        
    }
    catch {
        return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid form content");
    }
    
    try {
        addr = new MailAddress(order.Email);
        if(addr.Address != order.Email)
        {
            return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Email address");
        }
    }
    catch {
        return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Email address");
    }

    try {
        if(!order.Address.Contains("QLD") && !order.Address.Contains("Queensland"))
        {
            return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Only deliver to Queensland. Verify your address is in QLD.");
        }
    }
    catch {
        return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid address - verify you have provided a valid address in QLD.");
    }

    return req.CreateResponse(HttpStatusCode.OK, new {
        valid = true
    });
}

public class Order {
    public string Name {get; set;}
    public string Address {get; set;}
    public string Email {get; set;}
    public string CreditCard {get; set;}
    public double Price {get; set;}
}

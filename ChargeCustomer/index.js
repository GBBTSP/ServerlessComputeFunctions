module.exports = function (context, req) {
     context.log('Webhook was triggered!');

    if(req["card"] != null)
    {
    //Charge the customer

        context.res = {
            status: 200,
            body: {
                "message": "Customer charged"
            }
        }
    }
    else 
    {
        context.res = {
            status: 400,
            body: {
                "message": "No credit card provided"
            }
        }
    }
    context.done();
};
module.exports = function (context, data) {
    context.log('Webhook was triggered!');

    var phrases = data.phrases;
    context.res = {
        body: phrases.join(", ")
    }
    context.done();
}
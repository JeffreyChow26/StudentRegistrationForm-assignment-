define(['dojo/dom', 'dojo/on', 'dojo/request', 'dojo/dom-form'], function (dom,on, request, domForm) {

    var form = dom.byId('LoginFormDojo');
    on(form, "submit", function (event) {
        event.stopPropagation();
        event.preventDefault();
        var data = domForm.toObject('LoginFormDojo');
        request.post("/User/Login", {
            data: data,
            method: 'POST',
            handleAs: 'json'
        }).then(function (result) {
            window.location = result.url;
        });
    });

    document.addEventListener("DOMContentLoaded", () => {
        let form = document.querySelector('Form');
        form.addEventListener('submit', (e) => {
            e.preventDefault();
            return false;
        });
    });

    dijit.byId("emailAddress").validator = function (value, constraints) {
        if (value == '') {
            return true;
        }
        return dojox.validate.isEmailAddress(value, constraints);
    }

});
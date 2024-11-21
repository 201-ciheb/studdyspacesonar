window.addEventListener("load", function () {
    debugger;
    var a = document.querySelector("a.PostLogoutRedirectUri");
    if (a) {
        window.location = a.href;
    }
    else{
        this.document.getElementById("redirect_message").innerHTML("You will be redirected in 5 minutes");
        setTimeout(function() {
            window.location='/';
        }, 5000);
    }
});

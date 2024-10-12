var ClsSettings = {
    GetAll: function () {
        Helper.AjaxCallGet("http://localhost:5129/api/Setting", {}, "json",
            function (data) {

                var d1 = document.getElementById('About');
                d1.innerHTML = data.aboutUs;

                var facebook = document.getElementById('likFacebook');
                facebook.attr("href", data.facebookLink);

                var instgram = document.getElementById('likInstagram');
                instgram.attr("href", data.instgramLink);
                var twitter = document.getElementById('likTwitter').attr("href", data.twitterLink);
                var youtube = document.getElementById('likYoutube').attr("href", data.youtubeLink);

            }, function () { });
    }
}
ClsSettings.GetAll();

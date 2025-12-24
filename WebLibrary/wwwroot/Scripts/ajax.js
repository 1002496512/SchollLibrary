
$("document").ready(
    function ()
    {
        $(".link").click(
            function () {
                let uri = "http://localhost:49690/Catalog/GetBookList/?"
                if (this.hasAttribute("data-authorId")) {
                    let ext = "authorId=" + this.getAttribute("data-authorId");
                    uri = uri + ext;
                }
                if (this.hasAttribute("data-ganreId")) {
                    let ext = "ganreId=" + this.getAttribute("data-ganreId");
                    uri = uri + ext;
                }
                $.ajax({
                    url: uri,
                    method: "GET",
                    dataType: "html",
                    beforeSend: function () {
                        let loader = "<div class=loader>" +
                            "<img src='/Content/images/loader.gif'/>" +
                            "</div>";
                        $("#dataBar").html(loader);
                        
                    },
                    error: function () {
                        // your code here;
                    },
                    success: function (data) {
                        setTimeout(function () {
                          $("#dataBar").html(data);
                        }, 3000);
                        
                    },
                    complete: function () {
                        // your code here;
                    }
                });
            }
        );
    });







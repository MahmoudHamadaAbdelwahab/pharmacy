var ClsProduct = {
    GetAll: function () {
        Helper.AjaxCallGet("http://localhost:5129/api/Products", {}, "json",
            function (data) {
                var products = data.data || data;
                if (!Array.isArray(products)) {
                    console.error("Unexpected data format:", products);
                    $('#info').html('<p>Unexpected data format received.</p>');
                    return;
                }
                console.log(products);
                //$('#ItemPagination').pagination({
                //    dataSource: data.data,
                //    pageSize: 12,
                //    showGoInput: true,
                //    showGoButton: true,
                //    callback: function (data, pagination) {
                //        // template method of yourself
                //        var htmlData = "";

                //        for (var i = 0; i < data.length; i++) {
                //            htmlData += ClsProduct.DrawProduct(data[i]);
                //        }

                //        var d1 = document.getElementById('ItemArea');
                //        d1.innerHTML = htmlData;
                //    }
                //});

                    var htmlData = "";
                    products.forEach(function (product) {
                        htmlData += ClsProduct.DrawProduct(product);
                    });

                    console.log(htmlData);
                    var d1 = document.getElementById("ItemArea");
                    d1.innerHTML = htmlData;
                },
                function () {
                    console.error("AJAX call failed.");
                    $('#info').html('<p>An error has occurred while fetching products.</p>');
                });
            },
     }

    DrawProduct: function (item) {
        var productImages = '';

        if (item.TbImages && item.TbImages.length > 0) {
            item.TbImages.forEach(function (image, index) {
                productImages += `
                    <div class="carousel-item ${index === 0 ? 'active' : ''}">
                        <img src="/Uploads/Products/${image.ImageName}" class="d-block w-100" style="width: 100px; height: 100px;" alt="${item.ProductName}">
                    </div>`;
            });
        } else {
            // Default image if no images are available
            productImages = `
                <div class="carousel-item active">
                    <img src="/Uploads/Products/default.png" class="d-block w-100" style="width: 100px; height: 100px;" alt="Default Image">
                </div>`;
        }

        var data = `
            <div class="col-sm-6 col-lg-4 text-center item mb-4">

                <a href="/Store/Details?prodId=${item.ProductId}" class="btn btn-fw">
                    <span class="tag">Sale</span>
                    <div id="carouselExampleSlidesOnly" class="carousel slide" data-bs-ride="carousel">
                        <div class="carousel-inner">
                            ${productImages}
                        </div>
                    </div>
                    <h3 class="text-dark">${item.ProductName}</h3>
                    <p class="price"><del>95.00</del> &mdash; ${item.SalesPrice}</p>
                </a>
                <a href="/Order/AddToCart?itemId=${item.ProductId}" class="btn btn-success">Add to Cart</a>
            </div>`;

        return data;
    }
};

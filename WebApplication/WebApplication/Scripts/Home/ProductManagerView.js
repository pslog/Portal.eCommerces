var ProductManager = ProductManager || {};

ProductManager = {
    init: function () {
        /// <summary>
        /// Init functions for product board
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        ProductManager.bindEventForBotton();
    },
    bindEventForBotton: function () {
        /// <summary>
        /// Bind event for buttons in Product Board
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>


        //$("#deleteProductImage").unbind("click").bind("click", function () { ProductManager.RequestDeleteProductImage(productId, imageId); });
    },
    RequestDeleteProductImage: function (productId,imageId) {
        /// <summary>
        /// Delete a image from list images of product
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        $.ajax({
            url: "/Product/DeleteImage",
            dataType: "html",
            data: { productId: productId, imageId: imageId },
            success: function (result) {
                alert("delete image successful!!!");
                $("#EditProduct_ListProductImages").empty();
                $("#EditProduct_ListProductImages").html(result);
            },
            error: function (result) {
                alert("delete image error");
            }
        });
    },
    options: {
        selectedUsers: 0
    }
};
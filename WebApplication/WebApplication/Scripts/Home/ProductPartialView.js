var ProductBoard = ProductBoard || {};

ProductBoard = {
    init: function () {
        /// <summary>
        /// Init functions for product board
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        ProductBoard.bindEventForBotton();
    },
    addNewProductToCart:function(productId){
        /// <summary>
        /// Add a product to Cart
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        $.ajax({
            url: ProductBoard.options.path+"ProductBoard/AddToCart",
            dataType: "html",
            data: { Id: productId },
            success: function (result) {
                $("#ProductPartialView_CartModal .modal-body").empty();
                $("#ProductPartialView_CartModal .modal-body").html(result);
                $('#ProductPartialView_CartModal').modal('show');
            },
            error: function (result) {
                alert("add cart error");
            }
        });
    },
    removeFromCart: function (productId) {
        /// <summary>
        /// Remove a product to Cart
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        $.ajax({
            url: ProductBoard.options.path + "ProductBoard/RemoveFromCart",
            dataType: "html",
            data: { Id: productId },
            success: function (result) {
                $("#ProductPartialView_CartModal .modal-body").empty();
                $("#ProductPartialView_CartModal .modal-body").html(result);
                $('#ProductPartialView_CartModal').modal('show');
            },
            error: function (result) {
                alert("remove from cart error");
            }
        });
    },
    bindEventForBotton: function () {
        /// <summary>
        /// Bind event for buttons in Product Board
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        $("#ProductBoard_ContinueShopping").unbind("click").bind("click", function () { ProductBoard.ContinueShopping();})
    },
    ContinueShopping: function () {
        /// <summary>
        /// Do something when click ContinueShopping button in Cart Dialog
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        $('#ProductPartialView_CartModal').modal('hide');
    },
    options: {
        selectedUsers: 0,
        path: "http://localhost:11111/"
    }
};
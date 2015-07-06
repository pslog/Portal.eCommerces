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

        $("#ProductBoard_ContinueShopping").unbind("click").bind("click", function () { ProductBoard.ContinueShopping(); });
        $("#ProductBoard_CheckOut").unbind("click").bind("click", function () { ProductBoard.ShowCheckOutCartView(); });
    },
    ContinueShopping: function () {
        /// <summary>
        /// Do something when click ContinueShopping button in Cart Dialog
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        $('#ProductPartialView_CartModal').modal('hide');
    },
    UpdateQuantityProduct: function (quantity,productId) {
        /// <summary>
        /// Request to server when user change quantity of product
        /// server update quantity and total price for order and return to client
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        if (quantity <= 0) {
            alert("số lượng sản phẩm phải lớn hơn 0");
        }
        $.ajax({
            url: ProductBoard.options.path + "ProductBoard/UpdateQuantityOfProduct",
            dataType: "html",
            data: { Id: productId, quantity: quantity },
            success: function (result) {
                $("#ProductPartialView_CartModal .modal-body").empty();
                $("#ProductPartialView_CartModal .modal-body").html(result);
                $('#ProductPartialView_CartModal').modal('show');
            },
            error: function (result) {
                alert("cập nhật số lượng sản phẩm lỗi");
            }
        });
    },
    ShowCheckOutCartView:function(){
        /// <summary>
        /// Load CheckOut View
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        //$.ajax({
        //    url: ProductBoard.options.path + "ProductBoard/CheckOutCart",
        //    dataType: "html",
        //    type:"Get",
        //    success: function (result) {
        //        alert("check out cart show");
        //    },
        //    error: function (result) {
        //        alert("cập nhật số lượng sản phẩm lỗi");
        //    }
        //});
        $.get("ProductBoard/CheckOutCart");
    },
    options: {
        selectedUsers: 0,
        path: "http://localhost:11111/"
    }
};
var CheckOutCart = CheckOutCart || {};

CheckOutCart = {
    init: function () {
        /// <summary>
        /// Init functions for product board
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        CheckOutCart.bindEventForBotton();
    },
    removeFromCart: function (productId) {
        /// <summary>
        /// Remove a product to Cart
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        $.ajax({
            url:"/CheckOut/RemoveFromCart",
            dataType: "html",
            data: { Id: productId },
            success: function (result) {
                $("#ListProductInCart_CheckOutPage").empty();
                $("#ListProductInCart_CheckOutPage").html(result);
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

        $("#ProductBoard_ContinueShopping").unbind("click").bind("click", function () { CheckOutCart.ContinueShopping(); });
        $("#ProductBoard_CheckOut").unbind("click").bind("click", function () { CheckOutCart.ShowCheckOutCartView(); });
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
            url:"CheckOut/UpdateQuantityOfProduct",
            dataType: "html",
            data: { Id: productId, quantity: quantity },
            success: function (result) {
                $("#ListProductInCart_CheckOutPage").empty();
                $("#ListProductInCart_CheckOutPage").html(result);
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

        $.get("/CheckOut/OrderProduct");
    },
    options: {
        selectedUsers: 0
    }
};
namespace WebUI.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public List<CartItemModel>? CartItems { get; set; }
    }
}

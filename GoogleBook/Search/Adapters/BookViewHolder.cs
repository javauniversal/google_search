using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GoogleBook;
using GoogleBook.Search.Interface;

public class BookViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
{
    public ImageView imgBook { get; private set; }
    public TextView title { get; private set; }
    public TextView subtitle { get; private set; }
    private IItemsClickBook iItemsClickBook;

    public BookViewHolder(View itemView) : base(itemView)
    {
        // Locate and cache view references:
        imgBook = itemView.FindViewById<ImageView> (Resource.Id.imgBooks);
        title = itemView.FindViewById<TextView> (Resource.Id.titleBook);
        subtitle = itemView.FindViewById<TextView> (Resource.Id.subtitleBook);

        itemView.SetOnClickListener(this);
        itemView.SetOnLongClickListener(this);

    }

    public void setClickListener(IItemsClickBook iItemsClickBook)
    {
        this.iItemsClickBook = iItemsClickBook;
    }


    public void OnClick(View v)
    {
        this.iItemsClickBook.OnClickItems(v, AdapterPosition, false);
    }

    public bool OnLongClick(View v)
    {
        this.iItemsClickBook.OnClickItems(v, AdapterPosition, true);
        return true;
    }
}
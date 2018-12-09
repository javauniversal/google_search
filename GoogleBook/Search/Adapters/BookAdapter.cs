using System.Collections.Generic;
using System.Net;
using System.Runtime.Remoting.Contexts;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;
using GoogleBook;
using GoogleBook.Search.Interface;
using GoogleBook.Search.Models;

public class BookAdapter : RecyclerView.Adapter, IItemsClickBook
{
    private List<Book> listBook;
    private Activity contex;
    private int positionGlobal = 0;

    public BookAdapter(Activity contex, List<Book> listBook)
    {
        this.contex = contex;
        this.listBook = listBook;
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.items_book, parent, false);

        return new BookViewHolder(itemView);

    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) 
    {
        var book = listBook[position];
        BookViewHolder vh = holder as BookViewHolder;

        Glide.With(contex).Load(book.image).Apply(RequestOptions.CircleCropTransform()).Into(vh.imgBook);

        vh.title.Text = book.title;
        vh.subtitle.Text = book.price;
        vh.setClickListener(this);
    }

    public override int ItemCount
    {
        get { return listBook.Count; }
    }

    public void Update(List<Book> mLists)
    {
        listBook.Clear();
        listBook.AddRange(mLists);
        NotifyDataSetChanged();
    }

    public void OnClickItems(View v1, int adapterPosition, bool v2)
    {
        ItemDetail(adapterPosition);
    }

    private void ItemDetail(int position)
    {
        View view = contex.LayoutInflater.Inflate(Resource.Layout.layout_dialog, null);
        AlertDialog builder = new AlertDialog.Builder(contex).Create();
        builder.SetTitle("Book Detail");
        builder.SetView(view);
        builder.SetCanceledOnTouchOutside(false);
        builder.SetButton("Buy", handllerNotingButton);
        builder.SetButton2("Cancel", handllerNotingButtonCancel);

        ImageView imageView = view.FindViewById<ImageView>(Resource.Id.imgDetailBook);

        TextView titleBookDetail = view.FindViewById<TextView>(Resource.Id.titleBookDetail);
        TextView subtitleDetailBook = view.FindViewById<TextView>(Resource.Id.subtitleDetailBook);
        TextView isbn13DetailBook = view.FindViewById<TextView>(Resource.Id.isbn13DetailBook);
        TextView priceDetailBook = view.FindViewById<TextView>(Resource.Id.priceDetailBook);

        Glide.With(contex).Load(listBook[position].image).Apply(RequestOptions.CenterCropTransform()).Into(imageView);

        titleBookDetail.Text = listBook[position].title;
        subtitleDetailBook.Text = listBook[position].subtitle;
        isbn13DetailBook.Text = listBook[position].isbn13;
        priceDetailBook.Text = listBook[position].price;

        positionGlobal = position;

        builder.Show();
    }

    void handllerNotingButton(object sender, DialogClickEventArgs e)
    {
        AlertDialog objAlertDialog = sender as AlertDialog;
        var uri = Android.Net.Uri.Parse(listBook[positionGlobal].url);
        var intent = new Intent(Intent.ActionView, uri);
        contex.StartActivity(intent);

        objAlertDialog.Cancel();

    }

    void handllerNotingButtonCancel(object sender, DialogClickEventArgs e)
    {
        AlertDialog objAlertDialog = sender as AlertDialog;
        objAlertDialog.Cancel();

    }

}
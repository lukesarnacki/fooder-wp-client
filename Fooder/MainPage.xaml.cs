using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Xml.Linq;
using Microsoft.Phone.Tasks;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace Fooder
{
    public partial class MainPage : PhoneApplicationPage
    {
        CameraCaptureTask cameraCaptureTask;
        PhotoChooserTask photoChooserTask;
        private WebClient myWebClient = new WebClient();
        App thisApp = Application.Current as App;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);

            myWebClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(myWebClient_DownloadStringCompleted);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            /*if (!App.ProductsViewModelInstance.IsDataLoaded)
            {
                App.ProductsViewModelInstance.LoadData();
            }*/

            if (!App.IngredientsViewModelInstance.IsDataLoaded)
            {
                App.IngredientsViewModelInstance.LoadData();
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

            /* two types of handling button
             * photo chooser is needed for development
             */

            try
            {
                photoChooserTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("An error occurred.");
            }

            /*
            PhoneApplicationFrame root = Application.Current.RootVisual as PhoneApplicationFrame;
            //root.Navigate(new Uri("/Views/Camera.xaml", UriKind.Relative));
            try
            {
                cameraCaptureTask.Show();
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("An error occurred.");
            }
             * */
        }

        string Image = null;
        void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {

            App.IngredientsViewModelInstance.ProgressBarVisibility = "Visible";
            App.IngredientsViewModelInstance.IngredientsListVisibility = "Collapsed";

            if (e.TaskResult == TaskResult.OK)
            {
                //Code to display the photo on the page in an image control named myImage.
                System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.SetSource(e.ChosenPhoto);

                Image = Convert.ToBase64String(BitmapImageConverter.ConvertToBytes(bmp));


                var url = "http://fooder.developers.stoliczku.pl/labels.xml";
                //url = "http://posttestserver.com/post.php";

                // Create the web request object
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";

                webRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), webRequest);
                //myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myRequest);

            }
        }

        void myWebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

        }

        void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            // End the stream request operation
            Stream postStream = webRequest.EndGetRequestStream(asynchronousResult);

            // Create the post data
            // Demo POST data 
            // string postData = "image=" + "dupa";
            string postData = "image=" + Image;
                
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Add the post data to the web request
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();

            // Start the web request
            webRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), webRequest);
        }

        void GetResponseCallback(IAsyncResult asynchronousResult)
        {
                HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse response;

                // End the get response operation

                try
                {
                    response = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
                }
                catch (WebException e)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show("Wystąpił błąd, spróbuj ponownie"));
                    Dispatcher.BeginInvoke(() => App.IngredientsViewModelInstance.ProgressBarVisibility = "Collapsed");
                    Dispatcher.BeginInvoke(() => App.IngredientsViewModelInstance.IngredientsListVisibility = "Visible");
                    return;
                }

                Stream streamResponse = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamResponse);
                var Response = streamReader.ReadToEnd();
                streamResponse.Close();
                streamReader.Close();
                response.Close();

                XDocument xdoc = XDocument.Parse(Response);

                var ids = (from item in xdoc.Descendants("ingredient-id")
                          select item.Value);

                List<Int16> IngredientIds = new List<Int16>();
                foreach (var i in ids) IngredientIds.Add(Convert.ToInt16(i));

                Debug.WriteLine(Response);

                Dispatcher.BeginInvoke(() => App.IngredientsViewModelInstance.FilterIds(IngredientIds));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (thisApp.FilterIds)
            {
                thisApp.FilterIds = false;
                App.IngredientsViewModelInstance.CancelFilter();
                e.Cancel = true;
            }
            
        }
    }
}
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WpfApp1;


// メモ: このクラスはMVVMでいう、モデルビューを定義するクラスです。
// モデルの実態（インスタンス）はここに持っています（設計によってはモデルは他に置く場合があります）

// ここでは、以下の処理を設定したり、定義したりしています
// (1) 画像情報取得コマンド
// (2) 画像処理保存コマンド
// (3) 画像情報取得処理の実装
// (4) 画像情報保存処理の実装

// [デリゲートについて]
// デリゲートは「関数の部品化」と考えてみてください。ここでは(3) 画像情報取得処理(GetImageInfoModelList)を部品として、
// 画像情報取得ボタンのクリックイベントとバインドするGetImageInfoListCommandクラスに渡しています。
// GetImageInfoListCommandクラス側に「こういう形の関数を受け付けるよ=delegateメンバの宣言」があり、
// このファイル内で渡すときは、戻り値と引数が一致している関数を、あたかも変数のように渡しています。
// this.GetImageInfoListCommand = new GetImageInfoListCommand(this.GetImageInfoModelList);

namespace WpfApp1
{
	public class ImageManagerViewModel : INotifyPropertyChanged
	{
		// モデルの実態（インスタンス）
		private List<ImageInfoModel> imageInfoModels;
		public List<ImageInfoModel> ImageInfoModels
		{
			get { return imageInfoModels; }
			set { imageInfoModels = value; OnPropertyChanged(nameof(ImageInfoModels)); }
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			Debug.WriteLine($"OnPropertyChanged: {propertyName}");
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// (1) 画像情報取得コマンド：画像情報取得ボタンのクリックイベントで呼ばれる
		public ICommand GetImageInfoListCommand { get; }

		private readonly Dispatcher _dispatcher;
		private bool _isGettingImage = false;
		private readonly object _lockObject = new();

		public ImageManagerViewModel()
		{
			// モデルを初期化
			imageInfoModels = [];
			// 画像情報取得コマンドをインスタンス化、この時画像情報取得処理を渡す
			this.GetImageInfoListCommand = new GetImageInfoListCommand(this.GetImageInfoModelList, this.CanGetImage);
			_dispatcher = Dispatcher.CurrentDispatcher;
		}

		private bool CanGetImage()
		{
			return !_isGettingImage;
		}

		public void GetImageInfoModelList()
		{
			lock (_lockObject)
			{
				if (_isGettingImage) return;
				_isGettingImage = true;
			}

			try
			{
				if (_dispatcher.CheckAccess())
				{
					GetImageInfoModelListInternal();
				}
				else
				{
					_dispatcher.Invoke(GetImageInfoModelListInternal);
				}
			}
			finally
			{
				lock (_lockObject)
				{
					_isGettingImage = false;
				}

				CommandManager.InvalidateRequerySuggested();
			}

		}

		// (3) 画像情報取得処理の実装
		public List<ImageInfoModel> GetImageInfoModelListInternal()
		{
			Debug.WriteLine("start GetImageInfoModelList");

			List<ImageInfoModel> imageInfoModelList = [];

			ImageInfoModel imageInfoModel1 = new("image_1", "https://image-server-1/image_1.png", "Ichiro Yamada", "2025/9/1 14:00:00", 1, "");
			imageInfoModelList.Add(imageInfoModel1);


			ImageInfoModel imageInfoModel2 = new("image_2", "https://image-server-1/image_2.jpg", "Jiro Sato", "2025/9/3 16:00:00", 2, "");
			imageInfoModelList.Add(imageInfoModel2);

			ImageInfoModel imageInfoModel3 = new("image_3", "https://image-server-1/image_3.png", "Ichiro Yamada", "2025/9/1 15:00:00", 3, "");
			imageInfoModelList.Add(imageInfoModel3);

			ImageInfoModels = imageInfoModelList;

			Debug.WriteLine("end GetImageInfoModelList");

			return imageInfoModelList;
		}
	}
}

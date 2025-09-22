using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


// メモ: 
namespace WpfApp1
{
    public class GetImageInfoListCommand : ICommand
    {
        // こういう形の関数を部品化するよ宣言
        // デリゲートの宣言は部品の形を示すこと（コンセントとかの形のようなもの）
        public delegate void DelegateGetImageInfoModelList();

        // 部品化した関数をメンバとして宣言
        // デリゲートのメンバはコンセントそのもの（まだ部品が刺さっていない）
        public DelegateGetImageInfoModelList delegateGetImageInfoModelList;

        public delegate bool DelegateCanGetImage();

        public DelegateCanGetImage delegateCanGetImage;

        public GetImageInfoListCommand(DelegateGetImageInfoModelList delegateGetImageInfoModelList, DelegateCanGetImage delegateCanGetImage)
        {
            // 引数として渡された「関数の部品」をメンバ関数に設定
            // デリゲートのメンバに部品（関数の実態、実装）を接続（ここではコンセントに刺しただけでまだ関数は実行されない）
            this.delegateGetImageInfoModelList = delegateGetImageInfoModelList;
            this.delegateCanGetImage = delegateCanGetImage;
        }
        public bool CanExecute(object? parameter)
        {
            return this.delegateCanGetImage?.Invoke() ?? true;
        }

        public event EventHandler? CanExecuteChanged;

        // ここはUI部品のcommandプロパティと結合している。ボタンのクリックイベントで発火。
        public void Execute(object? parameter) 
        {
            MessageBox.Show("test");
            // 部品化した関数はInvokeで実行する
            delegateGetImageInfoModelList.Invoke();
        }

    }
}

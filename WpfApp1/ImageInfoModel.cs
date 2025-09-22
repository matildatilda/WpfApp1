using System;
using System.Runtime.InteropServices;

// メモ: このクラスはMVVMでいう、モデルを定義するクラスです
// モデルはデータの保持だけをするのでメンバーとgetter/setterだけでメソッドはありません。
// データを入れておく形の決まった箱のようなものです。
// このサンプルではモデルは１つですが実際は複数のモデルがあるはず。
namespace WpfApp1
{
    public class ImageInfoModel
    {

        private string imageName;   // 画像名
        public string ImageName { get { return imageName; } set { imageName = value; } }

        private string imageUrl;    // 画像ファイルURL
        public string ImageUrl { get { return imageUrl; } set { imageUrl = value; } }

        private string createdBy;   // 作成者
        public string CreatedBy { get { return createdBy; } set { createdBy = value; } }

        private string createdAt;    // 作成日時
        public string CreatedAt { get { return createdAt; } set { createdAt = value; } }

        private int confidentialMode;  // 機密 1:公開, 2: 社内外関係者のみ, 3: 社外秘
        public int ConfidentialMode { get { return confidentialMode; } set { confidentialMode = value; } }

        private string description;     // 説明
        public string Description { get { return description; } set { description = value; } }

        public ImageInfoModel()
        {
            this.imageName = "";
            this.imageUrl = "";
            this.createdBy = "";
            this.createdAt = "";
            this.confidentialMode = 1;
            this.description = "";
        }

        public ImageInfoModel(string imageName, string imageUrl, string createdBy, string createdAt, int confidentialMode, string description)
        {
            this.imageName = imageName;
            this.imageUrl = imageUrl;
            this.createdBy = createdBy;
            this.createdAt = createdAt;
            this.confidentialMode = confidentialMode;
            this.description = description;
        }
    }
}

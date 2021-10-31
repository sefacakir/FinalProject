using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        //isimlendirmeler düzeltilecek.
        public static string SuccessAdd = "Veri ekleme işlemi başarılı.";
        public static string kayitBulunamadi = "Kayıt bulunamadı.";
        public static string ErrorAdd = "Veri ekleme işlemi başarısız.";
        public static string SuccessDelete = "Veri silme işlemi başarılı.";
        public static string ErrorLengthPrice = "Veri açıklamasının en az 2 karakter ve günlük kira ücretinin 0'dan büyük olması gerekmektedir.";
        public static string SuccessUpdate = "Veri güncelleme işlemi başarılı.";
        public static string MaintenanceTime = "Sistem bakımdadır.";
        public static string Success = "İşlem başarılı.";
        public static string Error = "İşlem başarısız.";
        public static string NotEmpty = "Bu kısım boş bırakılamaz.";
        public static string MinLength = "Minimum 2 karakter girilmelidir.";
        public static string GreaterThan = "Sıfırdan (0) daha büyük bir değer girilmelidir.";
        public static string Empty = "Id alanı veritabanı tarafından otomatik atanmaktadır. Lütfen boş bırakınız.";
        public static string CarCountOfBrandError = "Bir markada en fazla 10 araç olabilir.";
        public static string ControlOfName = "Aynı açıklamaya sahip bir araç eklenemez.";
        public static string CategoryLimitExceded = "Kategori sınırı aşıldığı için sisteme yeni ürün eklenemez.";
        public static string CarImageLimitExceeded = "Bir aracın en fazla 5 tane görseli olabilir.";
    }
}

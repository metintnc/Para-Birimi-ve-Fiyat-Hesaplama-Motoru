# 🛒 Çoklu Para Birimi ve Fiyat Hesaplama Motoru

Bu proje, **Nesne Yönelimli Programlama (OOP)** prensipleri kullanılarak C# ile geliştirilmiş bir konsol tabanlı alışveriş simülasyonudur. Farklı para birimlerindeki ürünleri tek bir yerel para birimine (TL) çevirerek sepete ekler, KDV ve indirim hesaplamaları yapar ve kullanıcının bütçe kontrolünü sağlar.

## 🚀 Proje Hakkında

Bu uygulama, bir e-ticaret sitesinin arka planında çalışabilecek temel bir fiyat hesaplama motorunu simüle eder. Kullanıcıdan bir bütçe alır, ürün listesinden seçim yapmasını ister ve dinamik olarak sepet tutarını hesaplar.

**Temel Özellikler:**
* **Çoklu Para Birimi Desteği:** Dolar ve Euro cinsindeki ürünler, güncel kur baz alınarak (simüle edilmiş kurlar) TL'ye çevrilir.
* **Dinamik KDV Hesaplama:** Her ürün için rastgele oranlarda (%10-%20 arası) KDV eklenir.
* **Bütçe Kontrolü:** Kullanıcının girdiği bütçe aşılırsa işlem otomatik olarak iptal edilir.
* **İndirim Sistemi:** Sepet onaylandığında rastgele bir indirim kuponu tanımlanır ve toplam tutardan düşülür.
* **Hata Yönetimi:** Negatif sayı girişi veya geçersiz seçimlerde `try-catch` blokları ile programın çökmesi engellenir.

## 🛠️ Kullanılan Teknolojiler ve Yöntemler

* **Dil:** C# (.NET)
* **OOP Kavramları:**
    * **Operator Overloading (Operatör Aşırı Yükleme):** Sepete ürün eklemek (`S = S + Ü`) ve indirimli toplam fiyatı hesaplamak için `+` operatörü özelleştirilmiştir.
    * **Sınıf Yapısı (Classes):** `Ürünler`, `Sepet` ve `İslem` sınıfları ile sorumluluklar ayrılmıştır.
    * **Encapsulation (Kapsülleme):** Veri güvenliği ve erişim belirteçleri kullanılmıştır.
    * **Collections:** Ürünleri ve sepet içeriğini yönetmek için `List<T>` yapısı kullanılmıştır.

## 💻 Koddan Örnekler

Projenin en dikkat çekici yanı, sepet işlemlerinin matematiksel operatörler gibi ele alınmasıdır:

```csharp
// Sepete ürün ekleme işlemi (Operator Overloading)
public static Sepet operator +(Sepet S, Ürünler Ü)
{
    S.toplamfiyat += Ü.KDVliFiyatı;
    S.sepettekiler.Add(Ü);
    return S;
}

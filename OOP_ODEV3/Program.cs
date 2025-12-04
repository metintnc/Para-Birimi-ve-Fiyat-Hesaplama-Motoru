İslem i = new İslem();
Sepet S = new Sepet();
Ürünler Ü = new Ürünler();

string[] urunAdlari = { "Tişört", "Pantolon", "Ayakkabı", "Kulaklık", "Laptop", "Parfüm", "Sırt Çantası", "Kahve Makinesi", "Gözlük", "Saat" };
double[] urunFiyatlari = { 5, 20, 2200, 100, 600, 600, 700, 100, 800, 110 };
int[] adetsayısı = { 5, 3, 2, 10, 1, 4, 6, 2, 8, 3 };
string[] parabirimi = { "Euro", "Dolar", "TL", "Dolar", "Euro", "TL", "Dolar", "Euro", "TL", "Dolar" };

for (int j = 0; j < urunAdlari.Length; j++)
{
    Ürünler a = new Ürünler();
    a.EsyaName = urunAdlari[j];
    a.BirimFiyat = urunFiyatlari[j];
    a.EsyaSayısı = adetsayısı[j];
    a.ParaBirimi = parabirimi[j];
    İslem.eşyalar.Add(a);
}

Console.WriteLine("Hoşgeldiniz.");
int bütçe=1;
bool dogrumu = false;
while (dogrumu == false)
{
    Console.WriteLine("Lütfen Alışveriş bütçenizi TL bazında giriniz");
    try
    {
        dogrumu = true;
        bütçe = Convert.ToInt32(Console.ReadLine());
        if (bütçe < 0)
        {
            Console.WriteLine("Geçersiz Sayı girdiniz");
            dogrumu = false;
        }
    }
    catch (FormatException)
    {
        dogrumu = false;
        Console.WriteLine("Geçersiz Sayı girdiniz");
    }
}
int secilen;
i.EşyalarıEkranayazdır();
while (true)
{
    try
    {

        Console.WriteLine("Almak İstediğiniz Ürünü seçiniz. (Sepeti Görüntülemek için 0'a Basınız.)");
        secilen = Convert.ToInt32(Console.ReadLine());
        if(secilen < 0|| secilen > 10)
        {
            throw new Exception("Geçersiz numara girdiniz");
        }
        else if (secilen == 0)
        {
            S.SepettekileriGoster();
            Console.WriteLine("Alışverişi Tamamla(1), Alışverişe Devam Et(2)");
            int secilen2 = Convert.ToInt32(Console.ReadLine());
            if (secilen2 != 1 && secilen2 != 2)
            {
                throw new Exception("Geçersiz numara girdiniz");
            }
            else if (secilen2 == 1)
            {
                Console.WriteLine($"Toplam Tutar: {S.toplamfiyat:0.00}TL , İndirim Kuponu: %{S.indirimoranı} , Ödenecek Tutar: {+S:0.00}TL");
                if (bütçe < S.toplamfiyat)
                {
                    Console.WriteLine("Alışveriş tutarı bütçenizi aştığı için iptal ediliyor.");
                }
                else
                {
                    Console.WriteLine("Siparişiniz ALınmıştır. En kısa sürede kargoya verilecektir.");
                }
                break;
            }
        }
        else
        {
            Console.Write("Sepete Ekleniyor");
            for (int a = 0; a < 3; a++)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.WriteLine("\n");
            Ü = İslem.eşyalar[secilen - 1];
            Ü.KDVhesapla();
            Ü.TlyeCevir();
            if (Ü.EsyaSayısı == 0)
                Console.WriteLine("Stokta kalmadığı için ürünü satın alamazsınız");
            else
            {
                S = S + Ü;
                Ü.EsyaSayısı -= 1;
            }
        }

    }
    catch (FormatException)
    {
        Console.WriteLine("Geçersiz sayı girdiniz");
    }
    catch (Exception e)
    {
        Console.WriteLine("Hata:" + e.Message);
    }
   

}


public class Ürünler
{
    public double KDVliFiyatı;
    public int KDVoranı {  get; set; }
    public string EsyaName {  get; set; }   
    public int EsyaSayısı {  get; set; }
    public double BirimFiyat {  get; set; }
    public string ParaBirimi {  get; set; }

    public void KDVhesapla()
    {
        Random rnd = new Random();
        KDVoranı = rnd.Next(10, 20);
        KDVliFiyatı = BirimFiyat * (100 + KDVoranı) / 100;
        
    }
    public void TlyeCevir()
    {
        if (ParaBirimi == "Dolar")
        {
            KDVliFiyatı = KDVliFiyatı * 41.85;
        }
        else if (ParaBirimi == "Euro")
        {
            KDVliFiyatı = KDVliFiyatı * 48.64;
        }
    }


}
public class İslem
{
    public static List<Ürünler> eşyalar = new List<Ürünler> { };
    
    public void EşyalarıEkranayazdır()
    {
        int sira = 1;
        foreach(var x in eşyalar)
        {
            Console.WriteLine($"{sira} - ÜrünAdı: {x.EsyaName} , Adet Sayısı: {x.EsyaSayısı} , Birim Fiyatı: {x.BirimFiyat:0.00}{x.ParaBirimi}");
            sira++;
        }
    }


}
public class Sepet
{
    public List<Ürünler> sepettekiler = new List<Ürünler> ();
    public double toplamfiyat = 0;
    public int indirimoranı;
    public int indirimliFiyat;
    public Sepet()
    {
        Random rnd = new Random();
        indirimoranı = rnd.Next(1, 50);
    }
    public static Sepet operator +(Sepet S,Ürünler Ü)
    {
        S.toplamfiyat += Ü.KDVliFiyatı;
        S.sepettekiler.Add(Ü);
        return S;
    }
    public void SepettekileriGoster()
    {
        int sira = 1;
        foreach(var x in sepettekiler)
        {
            Console.WriteLine($"{sira} - ÜrünAdı: {x.EsyaName} , Birim Fiyatı: {x.BirimFiyat:0.00}{x.ParaBirimi} , KDV Oranı: %{x.KDVoranı} , KDV'li Fiyatı: {x.KDVliFiyatı:0.00}TL");
            sira++;
        }
    }
    public static double operator +(Sepet a)
    {
        a.toplamfiyat = a.toplamfiyat * (100-a.indirimoranı)/100;
        return a.toplamfiyat;
    }
}
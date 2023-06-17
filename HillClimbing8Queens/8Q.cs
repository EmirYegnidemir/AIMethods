using System;
using System.Diagnostics;
using System.Linq;

namespace _8Queens
{
    public class Program
    {
        private const int N = 8;  // vezir sayısı sabit değişkeni
        public class EightQueens
        {
            public int satir;
            public int sutun;

            public EightQueens(int satir, int sutun)  // vezirin tahta üzerindeki satır ve sütununu temsil eden NQueen nesnesinin constructor metodu
            {
                this.satir = satir;
                this.sutun = sutun;
            }

            public bool tehditVarMi(EightQueens q)  // parametre olarak girilen ve üzerinde çağrılan iki vezirin birbirini tehdit edip etmediği kontrol ediliyor
                                                    // true ise iki taş birbirini alabilecek pozisyondadır
            {
                //  aynı sütun veya satırda ise
                if (satir == q.satir || sutun == q.sutun)
                    return true;
                //  çapraz olarak birbirlerini alabiliyorlar ise
                else if (Math.Abs(this.sutun - q.sutun) == Math.Abs(this.satir - q.satir))
                    return true;
                return false;
            }

            public void hamle()  // vezirin 1 satır üste hamle yapmasını sağlayan metot
            {
                satir++;
            }


        }

        public static int adimSay = 0; // hill climbing algoritmasında geçilen her bir tepe durumunun sayısının temsili
        public static int sezgi = 0;  // o anda tahtada kaç vezirin birbirini alabilir konumda bulunduğu bilgisini verir
        public static int randomRestarts = 0;
        public static int hamleSay = 0;  // vezirlerin kaç kez hareket ettirildiği (hamle metodunun kaç kez çağrıldığı)

        //başlangıçta ve takılma durumlarında kullanılacak olan random bir tahta oluşturma metodu
        public static EightQueens[] tahtaOlustur(Random rand)
        {
            EightQueens[] baslangicTahtasi = new EightQueens[N];
            for (int i = 0; i < N; i++)
            {
                baslangicTahtasi[i] = new EightQueens(rand.Next(N), i);
            }
            return baslangicTahtasi;
        }

        //Tahtanın durumunu vezirlerin bulunduğu kareleri 1 ile temsil ederek gösteren metot
        private static void tahtayiYazdir(EightQueens[] vezir)
        {

            int[,] tempBoard = new int[,] { {0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0 }, };
            for (int i = 0; i < N; i++)
            {
                tempBoard[vezir[i].satir, vezir[i].sutun] = 1;  // oluşturulan geçici tahtada vezir bulunan konumları 1 yapan metot
            }
            Console.WriteLine();
            for (int j = 0; j < N; j++)  // yazdırma işlemi gerçekleşen döngü
            {
                for (int i = 0; i < N; i++)
                {
                    Console.Write(tempBoard[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Bir vezirin toplam çakışma sayısını döndüren sezgi değeri bulmaya yarayan fonksiyon
        public static int sezgiBul(EightQueens[] state)
        {
            int heuristic = 0;
            for (int i = 0; i < state.Length; i++)
            {
                for (int j = i + 1; j < state.Length; j++)
                {  // 0la 1 zaten kıyaslandı, doğrudan 1 ile 2, 2 ile 3 şeklinde kıyaslamaya başlanmalı, çakışma durumlarında sezgi arttırıldı
                    if (state[i].tehditVarMi(state[j]))  // true ise yani çakışma varsa
                    {
                        heuristic++;  // eğer kıyaslanan vezirlerin tehdit durumu var ise sezgi değeri 1 arttırılır. Döngü sonunda tahtanın toplam sezgi değeri elde edilecektir. (program içinde kullanıldığında)
                    }
                }
            }
            return heuristic;
        }

        // Daha düşük bir sezgi değerli tahta elde etmek için kullanılacak olan metot
        public static EightQueens[] yeniTahta(EightQueens[] simdikiTahta, Random rand)
        {
            EightQueens[] geciciTahta = new EightQueens[N];
            EightQueens[] yeniTahta = new EightQueens[N];
            
            int mevcutSezgi = sezgiBul(simdikiTahta);  // şu anki tahtadaki toplam çakışma sayısını temsil ediyor
            int enİyiSezgi = mevcutSezgi;  // ve bu değer best olarak atanıyor
            int temp;

            for (int i = 0; i < N; i++)
            {
                //  şu andaki tahta hem en iyi hem de geçici tahtaya kopyalanıyor
                yeniTahta[i] = new EightQueens(simdikiTahta[i].satir, simdikiTahta[i].sutun);
                geciciTahta[i] = yeniTahta[i];
            }
            // bu döngü programın en can alıcı kısmı olup açıklaması şu şekildedir. İlk sefer için (tahtadaki ilk sütundaki (0.) vezir) ilk vezir 0. satıra getirilir (kendi sütunu içinde) ve sezgi kontrolü yapılır. Düşük sezgi elde edilmezse tek tek yukarı satıra çıkarılır.
            // ilk durumdan sonraki durumlar için önceki sefer ilk satıra getirilen vezir tekrar ilk alındığı konuma getirilir. Bu sefer ilgilenilen vezir kendi sütununda 0. satıra getirilip aynı kontroller sırasıyla yapılır. Bir sonraki turda yeri değiştirilen vezir tekrar ilk
            // haline getirilir, o turda ilgilenilen vezir (i değerinin temsil ettiği sütundaki vezir) ise tekrar 0. satıra getirilip gerekli sezgi kontrolleri yapılır.
            // bütün çabalar sonucu daha iyi sezgi değeri elde edilemiyorsa bir işlem yapılmamış olur. Random restart'a geçilmesi döngü dışına çıkıldığında gerçekleştirilecektir.
            for (int i = 0; i < N; i++)
            {
                if (i > 0)  
                    geciciTahta[i - 1] = new EightQueens(simdikiTahta[i - 1].satir, simdikiTahta[i - 1].sutun);
                geciciTahta[i] = new EightQueens(0, geciciTahta[i].sutun);  // şu andaki tahtanın ilk vezir nesnesi bulunduğu sütunun ilk satırına (0.) kaydırılır. Bu yeni durum için sezgi kontrolü yapılacak.
                for (int j = 0; j < N; j++)
                {
                    temp = sezgiBul(geciciTahta);
                    if (temp < enİyiSezgi)  // önceki elde edilen sezgi değerinden daha iyi bir sezgi değerine ulaşıldıysa bu tahta yeni tahta olarak atanacaktır
                    {
                        enİyiSezgi = temp;
                        adimSay++;
                        // tahtanın kopyalanma işlemi
                        for (int k = 0; k < N; k++)
                        {
                            yeniTahta[k] = new EightQueens(geciciTahta[k].satir, geciciTahta[k].sutun);
                        }
                    }
                    // vezirin konumu güncellenir
                    if (geciciTahta[i].satir != N - 1)  // taşmanın önlenmesi
                    {
                        geciciTahta[i].hamle();
                        hamleSay++;
                    }
                }
            }
            //Yerel optimum'a takılma durumunun kontrolü sağlanıyor ve eğer takılma varsa random restart işlemi tahtaOlustur metodu sayesinde oluşturulan yeni tahta dizilimi ile yeni bir sezgi değeri elde ediliyor. 
            if (enİyiSezgi == mevcutSezgi)
            {
                randomRestarts++;
                yeniTahta = tahtaOlustur(rand);
                sezgi = sezgiBul(yeniTahta);
            }
            else
                sezgi = enİyiSezgi;
            adimSay++;
            return yeniTahta;
        }

        public static void Main(String[] args)
        {
            long[] ortSure = new long[15];
            int[] ortRest = new int[15];
            int[] ortYer = new  int[15];
            int basarisiz = 0;
            Stopwatch sayac = new Stopwatch();
            Random rand = new Random();
            int cözümSay = 1; // problemin kaçıncı kez çözüldüğünü belirten değişken
            for (int z = 0; z < 15; z++)  // problemin 15 kez çalıştırılması istendiğinden for döngüsü ile sağlandı
            {
                sayac.Start();
                int sezgiDegeri;
                Console.WriteLine(z + 1 + ". denemenin çözümü:");
                Console.WriteLine("");
                //ilk tahtayı rastgele olarak oluştur
                EightQueens[] simdikiTahta = tahtaOlustur(rand);
                Console.WriteLine(z + 1 + ". denemenin ilk durumu:");
                Console.WriteLine("");
                tahtayiYazdir(simdikiTahta);
                Console.WriteLine("");
                sezgiDegeri = sezgiBul(simdikiTahta);
                Console.WriteLine("İlk durumdaki sezgi değeri: " + sezgiDegeri);
                // en iyi durum ilk tahtada oluştuysa işlem yapılmasına gerek yok
                while (sezgiDegeri != 0)
                {
                    //  sıradaki tahtayı oluştur
                    simdikiTahta = yeniTahta(simdikiTahta, rand);
                    sezgiDegeri = sezgi;
                }
                sayac.Stop();
                //çözüm:
                Console.WriteLine("");
                Console.WriteLine("Çözüm " + cözümSay);
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine("");
                tahtayiYazdir(simdikiTahta);
                Console.WriteLine("\nAlgoritmada geçilen toplam tepe sayısı: " + adimSay);
                Console.WriteLine("Tıkanılan durumlarda yapılan toplam random restart sayısı: " + randomRestarts);
                Console.WriteLine("Toplam vezir hamlesi sayısı: " + hamleSay);
                Console.WriteLine("Geçerli çözüm için geçen süre (ms): " + sayac.ElapsedMilliseconds);
                Console.WriteLine("Son sezgi değeri: " + sezgiDegeri);
                if (sezgiDegeri != 0)
                    basarisiz++;
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine("");

                // tablo şeklinde verilerin yazdırılması için 3 ayrı veri için 3 ayrı array oluşturuldu:
                ortRest[cözümSay - 1] = randomRestarts;
                ortSure[cözümSay - 1] = sayac.ElapsedMilliseconds;
                ortYer[cözümSay - 1] = hamleSay;
                sayac.Reset();
                cözümSay++;
            }
            // istenen tablonun yazdırılma işlemi:
            Console.WriteLine("                             Toplam Random Restart:                                 Toplam Süre:                                 Toplam Hamle:");
            Console.WriteLine("                             ----------------------                                 -------------                                ------------");
            for (int z = 0; z < cözümSay-1; z++)
            {
               
                Console.WriteLine(z+1 + ". Çözüm için:  \t\t\t\t" + ortRest[z] + "   \t\t\t\t\t\t" + ortSure[z] + "  \t\t\t\t\t" + ortYer[z]);               
            }
            int ortRestartSay = ortRest.Sum() / 15;
            long ortGecenSure = ortSure.Sum() / 15;
            int ortYapilanHamle = ortYer.Sum() / 15;
            Console.WriteLine("Ortalamalar:  \t\t\t\t\t" + ortRestartSay + "   \t\t\t\t\t\t" + ortGecenSure + " \t\t\t\t\t" + ortYapilanHamle);
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Başarısızlık durumu sayısı: " + basarisiz);  // bu değerin asıl işlevi tablo yazımının comment haline getirilip programın 1000 kez çalıştırılmasıyla sezginin 0'a indirgenemediği durumların ortaya
                                                                            // çıkıp çıkmadığının kontrolü içindir. 1000 kez ve 10 sefer 100er kez çalıştırılma durumlarında 0 dışında bir veriye rastlanmadı. 

        }
    }

}

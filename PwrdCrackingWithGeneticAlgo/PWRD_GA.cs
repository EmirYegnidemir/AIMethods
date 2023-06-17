using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace GA
{
    // programın temel mantığı:
    // iyi olan bireylerin sıralanması ve seçimi (eşleşen karakterlere göre sıralama) (seçimde elitizm uygulandı) ilk %50 kesimden çocuk oluşumu sağlandı
    // seçilen ebeveynlere göre çaprazlama işlemi ve çocuk oluşumu
    // rastgele genin belirli olasılığa göre değiştirilmesi (mutasyon)
    
    class kromozomSinifi
    {
         
        Random rand = new Random();
        const string asilSifre = "Deep Learning 2022";
        //const string asilSifre = "DeepLearning";
        int sifreUzunlugu = asilSifre.Length;
        const string GENES = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 ";  // rastgele karakter seçileceği zaman bu string serisinden rastgele bir karakter seçilecek
        public kromozomSinifi()  // parametresiz constructor metodu
        {
            this.kromozom = this.kromozomOlustur();
            this.fitnessPuani = this.fitnessHesapla();
        }
   
        public int fitnessHesapla()
        {
            int fitness = 0;
            
            for (int j = 0; j < sifreUzunlugu; j++)
            {
                if (asilSifre[j] != this.kromozom[j])
                {
                    fitness++;
                }
            }
            return fitness;
        }

        public char mutasyonluGen()
        {
            int index = rand.Next(GENES.Length);
            char gen = GENES[index];
            return gen;
        }              
       
        public char[] kromozomOlustur()
        {
            char[] yeniKromozom = new char[sifreUzunlugu];
            for(int j = 0; j < sifreUzunlugu; j++)
            {
                char gen = mutasyonluGen();
                yeniKromozom[j] = gen;

            }
            return yeniKromozom;
        }

        public kromozomSinifi crossOver(kromozomSinifi parent2)
        {
            double olasilik = rand.NextDouble();
            kromozomSinifi cocuk = new kromozomSinifi();
            for (int i = 0; i < sifreUzunlugu; ++i)
            {
                olasilik = rand.NextDouble();
                if (olasilik < 0.45)
                {
                    cocuk.kromozom[i] = this.kromozom[i];
                }
                else if(olasilik < 0.9)
                {
                    cocuk.kromozom[i] = parent2.kromozom[i];
                }
                else
                {
                    cocuk.kromozom[i] = this.mutasyonluGen();
                }
            }
            return cocuk;    
        }

        public char[] kromozom { get; set; }
        public int fitnessPuani { get; set; }
        public kromozomSinifi(char[]genler)  // constructor metodu
        {
            this.kromozom = genler;
            this.fitnessPuani = this.fitnessHesapla();
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            Stopwatch sayac = new Stopwatch();
            Random rand = new Random();
            const int POPSAY = 200;
            int nesilSay = 1;
            int ortNesilSay = 0;

            bool bulundu = false;
            for (int z = 0; z < 3; z++) {
                sayac.Start();
                List<kromozomSinifi> populasyon = new List<kromozomSinifi>();
                for (int j = 0; j < POPSAY; j++)
                {
                    kromozomSinifi kromozom = new kromozomSinifi();
                    populasyon.Add(kromozom);
                }
                string kromozomString = new string(populasyon[0].kromozom);
                int kromozomFitness = populasyon[0].fitnessPuani;
            
                bulundu = false;
                nesilSay = 1;
                while (!bulundu)
                {
                    List<kromozomSinifi> siraliListe = populasyon.OrderBy(x => x.fitnessPuani).ToList();

                    populasyon = siraliListe;
                    if (populasyon[0].fitnessPuani == 0) //eğer sıralı halde ilk elemanın fitness puanı 0 ise hedefe ulaşılmış demektir
                    {
                        bulundu = true;
                        break;
                    }

                    List<kromozomSinifi> yeniNesil = new List<kromozomSinifi>();

                    //elitism: fitlik puanı en yüksek %10 popülasyon sonraki nesle aktarılır.
                    int i = ((10 * POPSAY) / 100);
                    List<kromozomSinifi> kopya = new List<kromozomSinifi>();
                    for (int k = 0; k < i; k++)
                    {
                        kopya.Add(populasyon[k]);
                    }
                    yeniNesil.AddRange(kopya);

                    // en yüksek fitlik puanına sahip neslin yüzde 50si ise kendi aralarında eşlenerek sonraki nesil oluşturulur
                    i = ((90 * POPSAY) / 100);
                    kromozomSinifi parent1 = new kromozomSinifi();
                    kromozomSinifi parent2 = new kromozomSinifi();
                    kromozomSinifi cocuk = new kromozomSinifi();
                    int randomSecim = 0;
                    for (int l = 0; l < i; l++)
                    {
                        randomSecim = rand.Next(51);
                        parent1 = populasyon[randomSecim];
                        randomSecim = rand.Next(51);
                        parent2 = populasyon[randomSecim];
                        cocuk = parent1.crossOver(parent2);
                        cocuk.fitnessPuani = cocuk.fitnessHesapla();
                        yeniNesil.Add(cocuk);
                    }

                    populasyon = yeniNesil;
                    List<kromozomSinifi> siraliListe2 = populasyon.OrderBy(x => x.fitnessPuani).ToList();
                    kromozomString = new string(siraliListe2[0].kromozom);
                    kromozomFitness = populasyon[0].fitnessPuani;
                    Console.WriteLine(" generation:{0} , String: {1} , Fitness = {2}", nesilSay, kromozomString, kromozomFitness);
                    nesilSay++;
                }
                sayac.Stop();
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine("Son nesil sonucu:\n");
                Console.WriteLine("");
                kromozomString = new string(populasyon[0].kromozom);                                                                         
                kromozomFitness = populasyon[0].fitnessPuani;               
                Console.WriteLine(" generation:{0} , String: {1} , Fitness = {2}", nesilSay, kromozomString, kromozomFitness);
                Console.WriteLine("");
                Console.WriteLine("Hesaplama için geçen süre (ms): " + sayac.ElapsedMilliseconds);
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("");
                ortNesilSay += nesilSay;
                sayac.Reset();
            }
            double ort = ortNesilSay / 3;
            Console.WriteLine("Ortalama kaç nesilde bulunduğu: " + ort);
        }



    }
}

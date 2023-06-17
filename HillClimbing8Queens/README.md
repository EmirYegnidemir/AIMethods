# HillClimbing8Queens
## Problemin Tanımı:
   Bu aşamada problem olarak 8 Queens probleminin hill climbing algoritması ile çözümü seçilmiştir. Problem 8 adet vezirin 8x8’lik bir satranç tahtasına hiçbir ikili vezirin birbirini tehdit etmeyecek biçimde yerleştirilmesi ile çözüme ulaşmaktadır.Program C# dilinde VisualStudio IDE’sinde yazılmıştır.
   
## Programın Özellikleri:
Program hill climbing metodu ile oluşturulmuş olup %97 gibi bir doğruluk oranıyla başarıya ulaştığı kanıtlanmıştır. (https://www.geeksforgeeks.org/n-queen-problem-local-search-using-hill-climbing-with-random-neighbour/) Satranç tahtasındaki her bir veziri bir state olarak ele alıp her bir veziri temsil eden bir nesne oluşturulması ve bu nesnelerin satır/ sütun değerlerine sahip olmasıyla satranç tahtasındaki dağılımları temsil edilmiştir. Vezirlerin birbirlerini tehdit etmemesi için aynı satır, sütun veya çaprazlama doğrultuda olmamaları gerekmektedir. Bu durumun kontrolü vezir nesnelerinin satır/ sütun değerlerinin kontrolüyle kolaylıkla sağlanmıştır.

if (satir == q.satir || sutun == q.sutun)
                    return true;
                //  çapraz olarak birbirlerini alabiliyorlar ise
 else if (Math.Abs(this.sutun - q.sutun) == Math.Abs(this.satir - q.satir)) 
                    return true;
                    
Her bir tehdit durumunun true olarak döndürülmesiyle sezgi değeri 1 arttırılmıştır. Bu kontrolün tüm vezir çiftleri için kıyaslanmasıyla tahtanın o andaki durumu için geçerli sezgi değeri elde edilmiştir. Programın ana amacı bu sezgi değerini 0 yapmaktır. Böylelikle tehdit yani vezirlerin çakışma durumları ortadan kaldırılmış olacaktır. Bunun için ise hamle metodu kullanılarak geçerli vezirin satır sayısını aynı sütunda kalarak 1 arttırılarak yeni bir durum oluşturulup bu durum için sezgi kontrolü yapılacaktır. Önceki sezgi değeri ile yapılan kıyaslamalar sonucu daha iyi bir duruma (daha düşük bir sezgiye) ulaşılırsa sonraki turda bu tahtadan devam edilecektir. Aynı zamanda daha iyi bir sezgiye ulaşılması demek hill climbing içinde bir tırmanma hareketi olarak da tanımlanabilir ve programda bu değer her çözüm sonunda yazdırılmıştır. Vezirlerin yaptıkları toplam hamleler doğal olarak çok daha büyük bir değer olmuştur. Satranç tahtasını temsil eden 8x8lik matrisin yanında vezirlerin değerlerini tutan bir vezirler matrisi de ayrıca tanımlanmıştır. Tahtayı temsil eden matris, vezirlerin konumlarının ifade edildiği 1 rakamı doldurularak işlemler sonunda çözümün görüntülenebilmesini sağlamıştır. Programın ana yapısı ise yeniTahta() fonksiyonu sayesinde oluşmuştur. Daha iyi bir tahta diziliminin olup olmadığı bu metot sayesinde bulunacaktır. Daha iyi durum bulunduğunda geçerli tahtanın yerine bulunan dizilim kaydedilecektir. Program 2 adet sınıf ile oluşturulmuştur. (program ana sınıfı es geçilecek olursa) EightQueens sınıfı ile vezirlerin tahtada yer aldıkları konumlar kaydedilmiştir. İçinde yer alan tehditVarMi metodu ile herhangi bir kesişmenin olup olmadığı kontrol edilmiştir. Hamle metodu ile üzerinde çağrılan vezirin 1 satır yukarı hareket ettirilmesi sağlanmıştır.(daha iyi sezgi değeri ararken kullanılıyor) tahtaOlustur metodu rastgele biçimde her bir sütuna birer vezir yerleştirerek başlangıç tahtasını oluşturacaktır. tahtayiYazdir metodu ise hem başlangıçta hem de çözüm durumunda oluşan tahta görünümünü göstermeyi sağlamaktadır. Bunun için kaydedilen matrisi ekrana 8x8 şekilde yazdıracaktır. sezgiBul metodu ile vezirlerin kaydedildiği matris parametre olarak verilerek vezirlerin çiftler halinde birbirlerini tehdit edip etmedikleri bulunacaktır ve bu işlem için tehditVarMi metodu tekrar çağrılacaktır. Bu metotta kontrol aynı satır-sütun değeri veya kıyaslanan vezirin pozisyonunun farkının mutlak değeri ile eşlik bulunup bulunmaması ile sağlanmaktadır. yeniTahta metodu ile sezgi değerini global max yani 0 haline getirene kadar sezgi kıyaslaması yapılarak sütunlarda bulunan vezirlerin satır konumları değiştirilir ve oluşan yeni durumdaki sezgi değerinin öncesine göre daha iyi olup olmadığı kontrol edilir. Daha iyi bir duruma ulaşıldıysa yeni tahta, bu durumdaki geciciTahta’daki konumların kopyalanmasıyla elde edilir. Sürekli olarak daha iyi bir sezgi değerine ulaşılması hedeflenir.Aşağıdaki satırlar ile local max durumu kontrolü sağlanır ve random restart uygulanması sağlanır. Random restart durumunda adı üzerinde rastgele bir şekilde baştan bir tahta oluşturulması gereklidir ve oluşturulan bu tahtanın sezgi değeri bulunur.

if (enİyiSezgi == mevcutSezgi)
            {
                randomRestarts++;
                yeniTahta = tahtaOlustur(rand);
                sezgi = sezgiBul(yeniTahta);
            }
            
Programda 15 kez farklı bir başlangıç durumundan çözüme ulaşılması hedeflenmiştir. Her turda (15 turdan birinde) başlangıç durumu olarak rastgele hazırlanan tahta ve sezgi değeri yazdırılır, son duruma ulaşıldığında bu aşamaya gelinene kadar geçen süre milisaniye cinsinden, son sezgi değeri (0), geçilen adım sayısı (climbing algoritmasında) ve karşılaşılan local max durumlarından kaçış için random restart çözümünün kaç defa kullanıldığı yazdırılmaktadır.

## Çözüm Başarısı/Çözüm Adımları
Çözümün %97 gibi bir doğruluk değerine sahip olduğu internetteki kaynaklarda belirtilmiş olup pratikte %100’e yakın olduğunu söylemek mümkündür. 1000 kez çalıştırılma sonucunda hiçbir durumda 0 dışında bir sezgi değerinde sonlanmamıştır. Random restart kullanımı olmasaydı bu konu hakkında farklı bir çıkarıma varılabilirdi. Çözümü detaylı açıklamak gerekir ise burada yeniTahta metodunun detaylandırılması uygun olacaktır:
Geçici ve yeni olmak üzere iki ayrı tahta oluşturulur. Burada tahtadan kasıt vezirlerin temsil edildiği tek boyutlu arraylerdir. Metoda parametre olarak girilen şimdiki mevcut tahtanın sezgi değeri bulunur. Mevcut tahta, hem geçici hem de yeni tahta arraylerine kopyalanır. İlk sütundan başlanarak 0. Sütun için ayrı bir işlem yapılması şartıyla sırasıyla vezirler kendi sütunlarında 0. Satıra kaydırılır, sırasıyla sezgi kontrolü yapılır, mevcut durumdan daha iyi bir sezgiye ulaşılana kadar geçerli vezirin satır değeri 1 arttırılır. Bu işlem vezir son satıra gelene veya daha iyi bir sezgi değeri elde edilene kadar sürer. Daha iyi sezgi elde edilmezse sonraki tur, bu vezir şimdikiTahta durumundaki ilk yerine yerleştirilir ve işlemler sonraki turdaki vezir için (i’nin temsil ettiği sütundaki) tekrarlanır. Daha iyi bir sezgi bulunamamasının dışında aynı sezgi değerinin bulunması durumu da vardır. Bütün vezir kombinasyonları denenip daha iyi sezgi bulunamamasında local max değerinde takılma olmuş demektir ve random restart, yani tahtanın random şekilde baştan oluşturulması ile en baştan ilgili işlemler tekrarlanarak bu durum aşılmaya çalışılacaktır. Burada yapılan işlemlerin takibi için adimSay ve randomRestarts değişkenleri kullanılacak olup, hamleSay vezir hareketi sayısını, adimSay her bir daha iyi sezgi elde edildiğinde yeni duruma geçişi ve randomRestarts da her bir random restart durumu sayısını temsil edecektir. Bu değerler hem çözüme ulaşıldığında hem de en son 15 deneme sonucunda yazdırılan tabloda ifade edilecektir. Tablo için 3 ayrı değeri temsil eden 3 ayrı array kullanılmıştır ve bunlar ortRest, ortSure ve ortYer arrayleridir. Çözümün doğru olması sezgi değerinin 0 olması ile ölçülür. Bu durum hiçbir çakışma olmadığını ifade eder. Programın işleyişinin etkilenmemesi için ortalama bulduran arrayler devre dışı bırakılıp program 1000 kez çalıştırıldı, hiçbir durumda başarısızlığa (son durumda 0 olması) rastlanmadı. Burada random restart’ın büyük rol oynadığı düşünülmektedir. Not: Geçilen tepe sayısı değeri rapor hazırlandıktan sonra olması gerektiği şekilde güncellendi, random restartta daha iyi sezgi elde edildiğinde tepe geçildiği detayı sonradan fark edildi.

## Ekran Görüntüleri
![image](https://user-images.githubusercontent.com/55255756/229647237-cfada21e-5db7-43ed-a190-3c90d5787434.png)
![image](https://user-images.githubusercontent.com/55255756/229647258-52f6f6eb-59f6-42a0-b8ce-afb0deb36fc8.png)
![image](https://user-images.githubusercontent.com/55255756/229647266-48e32a29-8d89-47df-8534-a538c348a07e.png)
![image](https://user-images.githubusercontent.com/55255756/229647284-c80a0129-a438-483f-8257-6df307aa8602.png)
![image](https://user-images.githubusercontent.com/55255756/229647291-2534ee74-9ab0-4f85-a225-3faa3a999a7e.png)
![image](https://user-images.githubusercontent.com/55255756/229647298-078ebc9e-ca38-4d28-afa5-b350be355cdc.png)
![image](https://user-images.githubusercontent.com/55255756/229647304-93448d6d-18c9-483b-833c-3ed4820dd9f1.png)
![image](https://user-images.githubusercontent.com/55255756/229647312-b1d23bb8-9bac-49f2-a934-664120ea1d97.png)
![image](https://user-images.githubusercontent.com/55255756/229647320-249441e2-b190-406d-8cab-b8923eaf8449.png)
![image](https://user-images.githubusercontent.com/55255756/229647328-f4c90dda-7994-4667-bab0-92a75d3f9854.png)
![image](https://user-images.githubusercontent.com/55255756/229647336-9052e5c8-52ec-41b7-87d0-673b2afebb80.png)
![image](https://user-images.githubusercontent.com/55255756/229647347-9bb7d692-feb8-4633-a191-fbc747114c72.png)
![image](https://user-images.githubusercontent.com/55255756/229647367-20de58ee-2a6d-4af8-843f-e27cae5a82c3.png)
![image](https://user-images.githubusercontent.com/55255756/229647381-7895f291-520a-4d06-b6aa-248bfadacf3e.png)
![image](https://user-images.githubusercontent.com/55255756/229647393-41027e3e-183a-4053-ad25-0c006b9ce646.png)
![image](https://user-images.githubusercontent.com/55255756/229647398-1e9dcbcc-e37f-438e-882b-07d4e6711507.png)



### AI METHODS DERSİ KAPSAMINDAKİ PROJELERDE KULLANILAN KAYNAKLAR:

https://www.researchgate.net/figure/Hill-Climbing-Search-8-Queen-Puzzle_fig8_260197544
https://www.geeksforgeeks.org/genetic-algorithms/
https://en.wikipedia.org/wiki/Autoencoder
https://www.analytics-link.com/post/2019/02/14/password-cracking-with-a-genetic-algorithm
http://www.veridefteri.com/2017/11/23/scikit-learn-ile-veri-analitigine-giris/
https://towardsdatascience.com/synthetic-data-generation-a-must-have-skill-for-new-data-scientists-915896c0c1ae
https://archive.ics.uci.edu/ml/datasets/wine
https://towardsdatascience.com/wine-data-set-a-classification-problem-983efb3676c9
https://towardsdatascience.com/machine-learning-observe-how-knn-works-by-predicting-the-varieties-of-italian-wines-a64960bb2dae
https://machinelearningmastery.com/introduction-to-style-generative-adversarial-network-stylegan/
https://paperswithcode.com/method/stylegan
https://en.wikipedia.org/wiki/StyleGAN
https://www.youtube.com/watch?v=-8hfnlxEPn4
https://en.wikipedia.org/wiki/GPT-3
https://en.wikipedia.org/wiki/A*_search_algorithm
https://www.geeksforgeeks.org/a-search-algorithm/
https://en.wikipedia.org/wiki/Tabu_search
https://towardsdatascience.com/optimization-techniques-tabu-search-36f197ef8e25
https://www.kdnuggets.com/2022/02/random-forest-decision-tree-key-differences.html#:~:text=The%20critical%20difference%20between%20the,work%20according%20to%20the%20output.
https://towardsdatascience.com/decision-trees-and-random-forests-df0c3123f991
https://stats.stackexchange.com/questions/285834/difference-between-random-forests-and-decision-tree
https://en.wikipedia.org/wiki/Alpha%E2%80%93beta_pruning
https://www.youtube.com/watch?v=l-hh51ncgDI
https://medium.com/analytics-vidhya/confusion-matrix-accuracy-precision-recall-f1-score-ade299cf63cd#:~:text=F1%20Score%20becomes%201%20only,0.857%20%2B%200.75)%20%3D%200.799.



# WineClassification
AI Methods Course Project (2nd year)

## Veri setinin ve problemin kısa anlatımı
Veri seti olarak UCI Machine Learning Repository içinde yer alan “wine data set” tercih edilmiştir. Çeşitli şarapların kimyasal analizlerinin kullanılarak kökenlerinin (sınıflandırmasının) belirlenmesi için kullanılmıştır. Bu veri setinin özelliklerinden bahsetmek gerekirse 178 örnek üzerinden yapılan gözlemlerden oluşan bu veri setinde 13 öznitelik yer almaktadır ve bir tür sınıflandırma probleminde kullanılacaktır. Aynı bölgede (İtalya) yetiştirilen örneklemin üç ayrı bitki çeşidi (İng. cultivar) kullanılarak yapıldığı göz önüne alınırsa dolayısıyla üç ayrı sınıf bulunmaktadır. Bu sınıflandırma için dört ayrı metot kullanılmıştır fakat  yalnızca 2 adet istendiğinden raporda sadece SVM ve decision tree yer almaktadır (kNN, SVM, random forest classification). Program, scikit-learn kütüphanesi kullanılarak ve pycharm IDE ortamında yazılmıştır. Wine veri setindeki her bir örneğin ilk özniteliği sınıf belirtecidir ve 3 ayrı sınıf bulunmaktadır. Tüm öznitelikler süreklidir ve detaylandırmak gerekirse özniteliklerin neyi ifade ettikleri aşağıda belirtilmiştir:
1)	Alkol
2)	Malik Asit
3)	Kalıntı (Ash olarak ifade edilmiş, kendim bu şekilde çevirdim)
4)	Kalıntının Alkalilik Ölçütü (Alcalinity)
5)	Manezyum
6)	Toplam Fenol miktarı
7)	Antioksidan (Flavanoid)
8)	Antioksidan olmayan fenoller
9)	Proanthocyanidin (antioksidan bir madde)
10)	Renk yoğunluğu
11)	Ton (hue) (ne kadar kırmızı o kadar düşük pH: yüksek asidite)
12)	OD280/OD315 değerleri (çeşitli şarapların protein miktarlarını belirlemede kullanılan bir kategorizasyon yöntemi)
13)	Prolin (bir çeşit aminoasit)

## İki Farklı Sınıflandırıcı için Sonuçlar: Hata Matrisleri, Tablo
### Decision tree için elde edilen sonuçlar:

![image](https://user-images.githubusercontent.com/55255756/229647947-5dd136f0-4581-4477-8b73-6c5b2d16ccdf.png)
![image](https://user-images.githubusercontent.com/55255756/229647958-ad767431-9309-4823-8329-50a2313f0880.png)

### SVM için elde edilen sonuçlar:
![image](https://user-images.githubusercontent.com/55255756/229647995-79b16c37-e3af-49ec-bb9d-2d436161ec0f.png)
![image](https://user-images.githubusercontent.com/55255756/229648006-13cb9cb9-7bdf-460f-b539-ed1daf606423.png)

## Kullanıcı tarafından verilen örneğin sınıflandırma ekran görüntüsü (konsol çıktıları)
Yapılan 4 ayrı sınıflandırma modeli için de aynı veri setinin sınıflandırılma işlemi yapıldı ve içlerinde en düşük doğruluk değerine sahip (0.72) Knn modeli dışında tüm modeller doğru sınıflandırmıştır. Bu yüzden doğru sınıflama yapan RF ve yanlış sınıflama yapan KNN modellerinin sonuçlarının gösterilmesinin yeterli olacağı düşünülmüştür.

### KNN:
![image](https://user-images.githubusercontent.com/55255756/229648079-64cf8754-f8e2-48d6-808a-0dd78b1ca593.png)

### RF: 
![image](https://user-images.githubusercontent.com/55255756/229648096-ea4e8e6a-1f5c-402f-bbd7-08d0e77e58e2.png)

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
https://medium.com/analytics-vidhya/confusion-matrix-accuracy-precision-recall-f1-score-ade299cf63cd#:~:text=F1%20Score%20becomes%201%20only,0.857%20%2B%200.75)%20%3D%200.799


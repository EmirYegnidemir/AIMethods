import numpy as np
from sklearn.datasets import load_wine
from sklearn.tree import DecisionTreeClassifier
from sklearn.model_selection import train_test_split
from sklearn.metrics import confusion_matrix
import matplotlib.pyplot as plt
from sklearn.model_selection import cross_val_score, cross_validate
from sklearn.neighbors import KNeighborsClassifier
from sklearn import svm
from sklearn import metrics
from sklearn.metrics import classification_report, accuracy_score
from sklearn.ensemble import RandomForestClassifier
from sklearn.model_selection import KFold

#veri seti olarak wine dataset tercih edildi, 13 öznitelik olup 3 farklı sınıf bulunmakta.
wine = load_wine()

#***************************************************************************************************************************************************************************************************************
# DECISION TREE CLASSIFICATION-----------------------------------------------------------------------------------------
print("***************************************************************************************************************************************************")
print("DECISION TREE CLASSIFICATION")
print("")
X = wine.data
y = wine.target

clf = DecisionTreeClassifier(random_state=0)
#Stratify değeri veri kümesinin etiket yüzdelerini korumak için kullanılıyor.
#random_state değeri sonuçların her seferinde aynı çıkmasını sağlamak için kullanılıyor.
X_train, X_test, y_train, y_test = train_test_split(X,y, train_size = 0.7, test_size =
0.3, random_state = 0, stratify = y)

clf.fit(X_train,y_train)
test_sonuc = clf.predict(X_test)
# konsol çıktısının temiz görünmesi için test sonuçları comment haline getirildi.
# print(test_sonuc)
cm = confusion_matrix(y_test, test_sonuc)
print("CM:")
print(cm)
print("")
print('Karar ağacı doğruluk değeri: ' + str(accuracy_score(test_sonuc, y_test)))
print("10 fold cross validation kullanılarak:")
score = cross_val_score(clf, wine.data, wine.target, cv=10)# k = 10 fold cross validation ile doğruluk değeri gösterildi.
print("%0.2f standart sapmayla elde edilen doğruluk değeri %0.2f" % (score.std(),score.mean()))

#precision score  # burada micro parametresi ile ortalama hesabı yapılmasının gerçek sonucu değiştirmediği belirtildiği için tercih edildi,
#özel parametre bildirmeden yapıldığında hata mesajı ile karşılaşılmakta
print("precision:",metrics.precision_score(y_test,y_pred=test_sonuc,average='micro'))
#recall score
print("recall",metrics.recall_score(y_test,y_pred=test_sonuc,average='micro'))
print("")

# kullanıcı tarafından oluşturulmuş test verisi ve sınıflandırılma sonuçları
print("Oluşturulan test verilerinin SVM sınıflandırıcısı kullanılarak sınıflandırılması:")
manual_test = ([12,1,2,16,100,2,1,0.6,0.4,3.2,1.2,1.6,670],[14,2,2,16,120,3,3,0.2,3,5.6,1.2,4,1000])
print("ilk veri seti: ", manual_test[0])
print("ikinci veri seti: ", manual_test[1])
# 1st element should be classified as 1 and second one should be 2 at the end of evaluation.
test = clf.predict(manual_test)
print("Oluşturulan test verisinin sınıflandırma sonuçları: (ilk verinin 1 ikinci verinin 0 olması gerekmekte)")
print(test)
print("")

plt.matshow(cm) #confusion matrix'in plot edilerek gösterimi sağlandı
plt.title('Confusion matrix')
plt.colorbar()
plt.ylabel('True label')
plt.xlabel('Predicted label')
plt.show()
print("")

print("DT modelinin son sonuçları: \n {}".format(classification_report(y_test, test_sonuc)))
#***************************************************************************************************************************************************************************************************************
#RANDOM FOREST CLASSIFIER---------------------------------------------------------------------------------------------------
print("***************************************************************************************************************************************************")
print("RANDOM FOREST CLASSIFICATION")
rf = RandomForestClassifier(random_state = 0)
rf.fit(X_train,y_train)
#test verisinin etiketleri elde ediliyor
y_pred_rf = rf.predict(X_test)

# CM yazdırılması
cm = confusion_matrix(y_test, y_pred_rf)
print("CM:")
print(cm)
plt.matshow(cm) #confusion matrix'in plot edilerek gösterimi sağlandı
plt.title('Confusion matrix')
plt.colorbar()
plt.ylabel('True label')
plt.xlabel('Predicted label')
plt.show()
print("")

# kullanıcı tarafından oluşturulmuş test verisi ve sınıflandırılma sonuçları
print("Oluşturulan test verilerinin SVM sınıflandırıcısı kullanılarak sınıflandırılması:")
manual_test = ([12,1,2,16,100,2,1,0.6,0.4,3.2,1.2,1.6,670],[14,2,2,16,120,3,3,0.2,3,5.6,1.2,4,1000])
print("ilk veri seti: ", manual_test[0])
print("ikinci veri seti: ", manual_test[1])
# 1st element should be classified as 1 and second one should be 2 at the end of evaluation.
test_sonuc = rf.predict(manual_test)
print("Oluşturulan test verisinin sınıflandırma sonuçları: (ilk verinin 1 ikinci verinin 0 olması gerekmekte)")
print(test_sonuc)

print("")
#y_pred_proba_rf = rf.predict_proba(X_test)  #olasılıksal olarak ifade etmek isteseydik bu satır kullanılacaktı
print('Rastgele orman accuracy değeri: ' + str(accuracy_score(y_pred_rf, y_test)))
print("")
print("10 fold cross validation kullanılarak:")
score = cross_val_score(rf, wine.data, wine.target, cv=10)# k = 10 fold cross validation ile doğruluk değeri gösterildi.
print("%0.2f standart sapmayla elde edilen doğruluk değeri %0.2f" % (score.std(),score.mean()))

#precision score  # burada micro parametresi ile ortalama hesabı yapılmasının gerçek sonucu değiştirmediği belirtildiği için tercih edildi,
#özel parametre bildirmeden yapıldığında hata mesajı ile karşılaşılmakta
print("precision:",metrics.precision_score(y_test,y_pred=y_pred_rf,average='micro'))
#recall score
print("recall",metrics.recall_score(y_test,y_pred=y_pred_rf,average='micro'))

features_rf = rf.feature_importances_  # verileri ayırt etmede önemli olan piksellerin gösterilmesi,
# 13 feature olduğundan reshape fonksiyonunda karesel bir görüntü elde edilememekte
features_rf = np.reshape(features_rf, (1,13))
features_rf = np.reshape(features_rf, (1,13))

plt.figure(figsize=(3,3))
plt.imshow(features_rf)
plt.title('Random Forest')
plt.colorbar()
plt.show()
print("")

print("RF modelinin son sonuçları: \n {}".format(classification_report(y_test, y_pred_rf)))

#***************************************************************************************************************************************************************************************************************
#SVM CLASSIFIER-------------------------------------------------------------------------------------------------------------------------
print("***************************************************************************************************************************************************")
print("SVM CLASSIFICATION")
cls = svm.SVC(kernel="linear")
#train model
cls.fit(X_train,y_train)
#predict
pred = cls.predict(X_test)

# CM yazdırılması
cm = confusion_matrix(y_test, pred)
print("CM:")
print(cm)
plt.matshow(cm) #confusion matrix'in plot edilerek gösterimi sağlandı
plt.title('Confusion matrix')
plt.colorbar()
plt.ylabel('True label')
plt.xlabel('Predicted label')
plt.show()
print("")

print("accuracy:",metrics.accuracy_score(y_test,y_pred=pred))

print("")
print("10 fold cross validation kullanılarak:")
score = cross_val_score(cls, wine.data, wine.target, cv=10)# k = 10 fold cross validation ile doğruluk değeri gösterildi.
print("%0.2f standart sapmayla elde edilen doğruluk değeri %0.2f" % (score.std(),score.mean()))

#precision score  # burada micro parametresi ile ortalama hesabı yapılmasının gerçek sonucu değiştirmediği belirtildiği için tercih edildi,
#özel parametre bildirmeden yapıldığında hata mesajı ile karşılaşılmakta
print("precision:",metrics.precision_score(y_test,y_pred=pred,average='micro'))
#recall score
print("recall",metrics.recall_score(y_test,y_pred=pred,average='micro'))
print(metrics.classification_report(y_test,y_pred=pred))

# kullanıcı tarafından oluşturulmuş test verisi ve sınıflandırılma sonuçları
print("Oluşturulan test verilerinin SVM sınıflandırıcısı kullanılarak sınıflandırılması:")
manual_test = ([12,1,2,16,100,2,1,0.6,0.4,3.2,1.2,1.6,670],[14,2,2,16,120,3,3,0.2,3,5.6,1.2,4,1000])
print("ilk veri seti: ", manual_test[0])
print("ikinci veri seti: ", manual_test[1])
# 1st element should be classified as 1 and second one should be 2 at the end of evaluation.
test_sonuc = cls.predict(manual_test)
print("Oluşturulan test verisinin sınıflandırma sonuçları: (ilk verinin 1 ikinci verinin 0 olması gerekmekte)")
print(test_sonuc)
print("")

print("SVM modelinin son sonuçları: \n {}".format(classification_report(y_test, pred)))

#***************************************************************************************************************************************************************************************************************
# KNN CLASSIFICATION------------------------------------------------------------------------------------------------------------------
print("***************************************************************************************************************************************************")
print("KNN CLASSIFICATION")
cli = KNeighborsClassifier(n_neighbors=1)
cli.fit(X_train, y_train)
y_pred = cli.predict(X_test)

# CM yazdırılması
cm = confusion_matrix(y_test, y_pred)
print("CM:")
print(cm)
plt.matshow(cm) #confusion matrix'in plot edilerek gösterimi sağlandı
plt.title('Confusion matrix')
plt.colorbar()
plt.ylabel('True label')
plt.xlabel('Predicted label')
plt.show()
print("")

print("Doğruluk değeri: {:.2f}".format(cli.score(X_test, y_test)))

print("")
print("10 fold cross validation kullanılarak:")
score = cross_val_score(cli, wine.data, wine.target, cv=10)# k = 10 fold cross validation ile doğruluk değeri gösterildi.
print("%0.2f standart sapmayla elde edilen doğruluk değeri %0.2f" % (score.std(),score.mean()))
#precision score  # burada micro parametresi ile ortalama hesabı yapılmasının gerçek sonucu değiştirmediği belirtildiği için tercih edildi,
#özel parametre bildirmeden yapıldığında hata mesajı ile karşılaşılmakta
print("precision:",metrics.precision_score(y_test,y_pred=y_pred,average='micro'))
#recall score
print("recall",metrics.recall_score(y_test,y_pred=y_pred,average='micro'))

# kullanıcı tarafından oluşturulmuş test verisi ve sınıflandırılma sonuçları
print("Oluşturulan test verilerinin SVM sınıflandırıcısı kullanılarak sınıflandırılması:")
manual_test = ([12,1,2,16,100,2,1,0.6,0.4,3.2,1.2,1.6,670],[14,2,2,16,120,3,3,0.2,3,5.6,1.2,4,1000])
print("ilk veri seti: ", manual_test[0])
print("ikinci veri seti: ", manual_test[1])
# 1st element should be classified as 1 and second one should be 2 at the end of evaluation.
test_sonuc = cli.predict(manual_test)
print("Oluşturulan test verisinin sınıflandırma sonuçları: (ilk verinin 1 ikinci verinin 0 olması gerekmekte)")
print(test_sonuc)

print("KNN modelinin son sonuçları: \n {}".format(classification_report(y_test, y_pred)))
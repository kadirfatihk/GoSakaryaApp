# GoSakaryaApp - Sakarya'nın Kalbinde Bir Uygulama

GoSakaryaApp, Sakarya'daki gezilecek yerler ve etkinlikler hakkında kapsamlı bilgi sunan bir ASP.NET web uygulamasıdır. Kullanıcılar etkinlikleri keşfedebilir, bilet alabilir, mekanlar hakkında bilgi edinebilir ve yorum yapabilirler.

## Özellikler

* **Kullanıcı Rolleri:** Admin ve Visitor
* **Etkinlik Yönetimi:** Etkinlik listeleme, bilgi görüntüleme, bilet alma
* **Mekan Bilgisi:** Gezilmesi önerilen yerler hakkında detaylı bilgi
* **Yorum Sistemi:** Kullanıcıların mekanlar ve etkinlikler hakkında yorum yapması
* **Admin Paneli:** Konum ve etkinlik ekleme, silme, güncelleme (POST, PATCH, PUT, DELETE), kullanıcı yorumlarını silme, bakım modu

## Teknolojiler

* **ASP.NET Core:** Web uygulama çatısı
* **C#:** Programlama dili
* **Entity Framework Core:** Veritabanı erişim teknolojisi
* **JWT (JSON Web Token):** Yetkilendirme
* **Data Protection:** Veri şifreleme
* **Swagger:** API dökümantasyonu (öneri)


## Mimari

Proje, temiz kod prensipleri ve katmanlı mimari göz önünde bulundurularak geliştirilmiştir.

* **Controllers:** HTTP isteklerini yönetir.
* **Models:**
    * **Entities:** Veritabanı varlıkları.
    * **DTOs (Data Transfer Objects):** Veri transferi için kullanılan nesneler.
    * **RequestModels:** API isteklerini temsil eden nesneler.
* **Services:** İş mantığını içerir.
* **Data:** Veritabanı erişim katmanı.
    * **Repositories:** Veritabanı işlemlerini soyutlar.
    * **UnitOfWork:** Veritabanı işlemlerinin tutarlılığını sağlar.
* **Filters:** `ActionFilter`, `TimeFilter` gibi filtreler ile HTTP istekleri üzerinde işlem yapar.  (Örneğin zaman kısıtlaması)
* **Middleware:** Uygulama seviyesinde işlemler yapmak için kullanılır. (Örneğin bakım modu)


## Desenler

* **Repository Pattern:** Veri erişim mantığını soyutlar.
* **Unit of Work Pattern:** Veritabanı işlemlerini tek bir birim olarak ele alır.
* **DTO Pattern:** Veri transferini optimize eder ve güvenliği artırır.


## Kurulum

1. Projeyi klonlayın.
2. Gerekli bağımlılıkları yükleyin.
3. `appsettings.json` dosyasındaki veritabanı bağlantı ayarlarını yapılandırın.
4. Veritabanı migrastonlarını çalıştırın.


## İletişim

[İletişim Bilgileri] (kadir.fatih96@gmail.com)


## Ekran Görüntüleri

![areas](https://github.com/user-attachments/assets/32d8a112-4fd2-44f1-96e6-fe1443563d90)
![auth_comments](https://github.com/user-attachments/assets/8e669065-1f84-44d5-afbe-90116ce5f57d)
![event_ticket_settings](https://github.com/user-attachments/assets/e80d4a34-1cf0-4fb3-87e6-d1ecd26cc3a9)


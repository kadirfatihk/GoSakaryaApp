GoSakaryaApp - Sakarya'nın Kalbinde Bir Uygulama
Giriş
GoSakaryaApp, Sakarya'daki gezilecek yerler ve etkinlikler hakkında kapsamlı bir bilgi sunmayı amaçlayan, ASP.NET, C# ve Entity Framework teknolojileri kullanılarak geliştirilmiş bir web uygulamasıdır. Kullanıcılar, uygulama sayesinde şehirdeki tüm etkinliklere göz atabilir, bilet satın alabilir, gezilecek yerler hakkında detaylı bilgi alabilir ve yorum yapabilirler.

Özellikler
Kullanıcı Rolleri: Uygulama, admin ve visitor olmak üzere iki farklı kullanıcı rolüne sahiptir.
Admin:
Konum ve etkinlik ekleme, silme, güncelleme (POST, PATCH, PUT, DELETE)
Kullanıcı yorumlarını silme
Uygulamayı bakım moduna alma (middleware)
Visitor:
Konum ve etkinlik listeleme
Yorum yapma
Etkinliklere bilet alma (kullanıcı başına bir bilet)
Güvenlik:
Yetkilendirme: JWT (JSON Web Token) mekanizması ile kullanıcı yetkilendirme işlemleri gerçekleştirilmektedir.
Şifreleme: Veri güvenliği için DataProtection kullanılmaktadır.
Zaman Kısıtlamaları: ActionFilter ve TimeFilter attribute'leri ile konum ve etkinlik ekleme işlemlerine zaman kısıtlamaları getirilmiştir.
Veritabanı: Entity Framework ile veritabanı bağlantısı kurulmuştur.
Performans: ServiceLifetimes ile hizmetlerin ömrü yönetilerek performans optimize edilmiştir.
Teknolojiler ve Desenler
ASP.NET: Web uygulaması geliştirme çerçevesi
C#: Programlama dili
Entity Framework: Veritabanı erişim katmanı
JWT: Kullanıcı yetkilendirme
DataProtection: Veri şifreleme
ActionFilter, TimeFilter: HTTP isteklerini filtreleme ve zamanlama
DTO (Data Transfer Object): Veri transferi için kullanılan hafif nesneler
Entities: Veritabanı tablolarına karşılık gelen nesneler
RequestModel: Kullanıcı isteklerini temsil eden nesneler
UnitOfWork: Birden fazla veri erişim işlemini tek bir transaction içinde yöneten tasarım deseni
Repository: Veritabanı erişim işlemlerini kapsular
Proje Yapısı
Controllers: HTTP isteklerini işleyen ve uygun aksiyonları gerçekleştiren sınıflar
Models:
Entities: Veritabanı tablolarına karşılık gelen C# sınıfları
DTOs: Veri transfer nesneleri
RequestModels: Kullanıcı isteklerini temsil eden nesneler
Services: İş mantığını içeren sınıflar
Data: Veritabanı erişim katmanı
Repositories: Veritabanı işlemlerini yapan sınıflar
UnitOfWork: Birden fazla veri erişim işlemini yöneten sınıf
Filters: HTTP isteklerini filtreleyen attribute'ler
Neden Bu Desenler Kullanıldı?
Bakım kolaylığı: Kodun farklı katmanlara ayrılması, değişikliklerin daha kolay yapılabilmesini sağlar.
Test edilebilirlik: Her katman ayrı ayrı test edilebilir.
Veri erişiminden soyutlama: İş mantığı, veritabanı detaylarından bağımsız hale gelir.
Performans: Gerekli verilerin sadece gönderilmesi, performansı artırır.
Güvenlik: Hassas bilgilerin doğrudan client'a gönderilmesi engellenir.

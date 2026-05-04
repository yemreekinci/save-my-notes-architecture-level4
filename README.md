# SaveMyNotes API

SaveMyNotes, modern yazılım mimarileri ve yapay zeka entegrasyonu ile geliştirilmiş gelişmiş bir not yönetim sistemi backendi projesidir.

---

## 🇹🇷 Türkçe Proje Özeti

### Mimari Yapı
Bu proje, sürdürülebilir ve test edilebilir bir yapı sunmak adına **Clean Architecture** prensipleri üzerine inşa edilmiştir:
- **Domain Driven Design (DDD):** İş mantığı, zengin domain modelleri ve kuralları ile merkeze alınmıştır.
- **CQRS (Command Query Responsibility Segregation):** Okuma ve yazma operasyonları MediatR kütüphanesi kullanılarak birbirinden ayrılmıştır.
- **Ef Core & Repository Pattern:** Veri erişimi soyutlanmış ve performanslı hale getirilmiştir.

### Öne Çıkan Özellikler
- **AI Tag Generation:** Groq LLM modeli entegrasyonu sayesinde, notun içeriğinden otomatik olarak en alakalı 5 adet etiket (tag) üretilir.
- **Bulk Delete:** Tekil silme işlemlerinin yanı sıra, performanslı bir şekilde çalışan toplu not silme (Soft Delete) özelliği mevcuttur.
- **Soft Delete & Enum Status:** Notlar veritabanından tamamen silinmez, enum tabanlı statü yönetimi ile 'Deleted' olarak işaretlenir.

### Teknolojiler
- .NET 10
- MediatR (CQRS)
- Entity Framework Core
- FluentValidation
- Groq Cloud AI API

---

## 🇺🇸 English Project Summary

### Architecture
This project is built on **Clean Architecture** principles to provide a sustainable and testable structure:
- **Domain Driven Design (DDD):** Business logic is centralized using rich domain models and rules.
- **CQRS (Command Query Responsibility Segregation):** Read and write operations are separated using the MediatR library.
- **EF Core & Repository Pattern:** Data access is abstracted and optimized for performance.

### Key Features
- **AI Tag Generation:** Using **Groq LLM** integration, 5 highly relevant tags are automatically extracted from the note content.
- **Bulk Delete:** In addition to single deletions, a high-performance bulk soft-delete feature is implemented.
- **Soft Delete & Enum Status:** Notes are not physically removed; they are flagged as 'Deleted' using an enum-based status management.

### Tech Stack
- .NET 10
- MediatR (CQRS Pattern)
- Entity Framework Core
- FluentValidation
- Groq Cloud AI API

---

## Kurulum / Installation

1. Projeyi klonlayın / Clone the project: `git clone https://github.com/username/SaveMyNotes.git`
2. API anahtarlarınızı `appsettings.json` dosyasına ekleyin / Add your API keys to `appsettings.json`:
   ```json
   "GroqSettings": {
     "ApiKey": "YOUR_GROQ_API_KEY"
   }
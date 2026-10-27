# 🧊 IceSlide

Unity ve C# ile geliştirdiğim, mobil için tasarlanmış 2D bir **buz kaydırma bulmaca oyunu**.

Oyuncu bir yöne hamle yaptığında karakter, bir duvara çarpana kadar durmadan kayar. Amaç, en az hamleyle yeşil hedef kareye ulaşmak.

![Oyun ekran görüntüsü](docs/screenshot.png)

## 🎮 Nasıl Oynanır

- **Ok tuşları** ile yön seç (mobilde kaydırma hareketi planlanıyor).
- Karakter, bir engele çarpana kadar o yönde kayar.
- Hedef kareye ulaştığında seviye tamamlanır.

## ⚙️ Teknik Detaylar

- **Motor:** Unity 6 (Universal 2D / URP)
- **Dil:** C#
- **Girdi:** Unity Input System
- **Arayüz:** TextMesh Pro

**Mimari:**
- **Mantık ile görüntü ayrı tutuldu.** Seviyenin durumu (duvarlar, oyuncu ve hedef konumu) `GridManager` içinde bir ızgara veri yapısında tutuluyor. Ekrandaki nesneler sadece bu verinin görsel karşılığı. Bu yapı, çözücü algoritmanın oyunun görsel katmanından bağımsız çalışabilmesini sağlıyor.
- **Seviyeler metin tabanlı haritalardan üretiliyor** (`#` duvar, `.` boş, `P` oyuncu, `G` hedef). Nesneler prefab'lardan çalışma anında oluşturuluyor.
- **Hareket, ızgara üzerinde hesaplanıyor.** Kayma sonucu `GetSlideEnd` fonksiyonu ile mantıksal olarak bulunuyor, ardından ekranda yumuşak bir animasyonla gösteriliyor.

## 📁 Proje Yapısı

```
Assets/
├── Scripts/
│   ├── GridManager.cs    # Seviye ızgarası, duvar kontrolü, kayma hesabı
│   └── PlayerMover.cs    # Girdi, hareket animasyonu, kazanma kontrolü
├── Prefabs/              # Duvar ve hedef prefab'ları
└── Scenes/
```

## 🗺️ Yol Haritası

- [x] Izgara tabanlı seviye sistemi
- [x] Buz kaydırma mekaniği
- [x] Kazanma kontrolü ve ekran mesajı
- [ ] **BFS çözücü:** seviyenin çözülebilirliğini ve en kısa çözümü hesaplama
- [ ] **Prosedürel seviye üretimi:** yalnızca çözülebilir seviyeleri tutma, zorluğu çözüm uzunluğuna göre ayarlama
- [ ] Hamle sayısına göre ⭐⭐⭐ puanlama
- [ ] Menü, seviye seçimi ve ilerleme kaydı
- [ ] Mobil dokunmatik kontroller (swipe)
- [ ] Google Play'de yayın

## 🚀 Projeyi Çalıştırma

1. Repoyu klonla ya da ZIP olarak indir.
2. Unity Hub'da **Add → Add project from disk** ile klasörü seç.
3. Unity 6 (6000.6.x) ile aç.
4. `Assets/Scenes/SampleScene` sahnesini aç ve ▶ Play'e bas.

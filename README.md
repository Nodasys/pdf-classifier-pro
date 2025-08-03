# PDF Classifier Pro

**PDF Classifier Pro** is a next-generation document redaction and classification tool developed by [Nodasys](https://nodasys.com), tailored for modern professionals, investigators, journalists, and government organizations. It allows precise control over sensitive PDF content by enabling secure redaction, automated OCR analysis, and structured document classification — all within a refined and fluid Windows-native interface.

## 🚀 Project Vision

PDF Classifier Pro aims to become the **definitive tool for classifying and redacting sensitive PDF documents**, with a focus on:
- Automatic text recognition (OCR) of scanned documents.
- Seamless interface and user experience (Fluent UI).
- Reliable redaction (visual + actual text removal).
- Licensing system (Free vs Pro features).
- Secure export, signing, and classification tagging.

Future goals include cross-platform support (macOS, Linux) and integration with AI-assisted sensitive content detection.

---

## 🖥️ Technology Stack

| Layer | Tech |
|-------|------|
| UI | WPF (.NET 8) + Fluent.Ribbon (Modern Fluent UI) |
| Core | C# / .NET 8 |
| PDF Viewer | PdfiumViewer (BSD) |
| Text extraction | PdfPig (MIT) |
| OCR | Tesseract OCR (Apache 2.0) |
| Redaction | PDFSharp / PdfPig |
| Licensing | Custom + Portable.Licensing (MIT) |
| Future Support | Avalonia UI for macOS/Linux |

---

## ✨ Features

### ✅ Free Version
- Load and view PDF documents
- Basic redaction tools (limited areas)
- Manual OCR initiation (if image-only)
- Basic classification tagging

### 💼 Pro Version
- Automatic OCR on file open
- Unlimited redactions
- Export to redacted + signed PDF
- Full classification panel (TOP SECRET, CONFIDENTIAL, etc.)
- Secure vault export
- Logging and document tracing

---

## 🧱 Architecture Overview

```
/PDFClassifierPro
│
├── /UI               # WPF UI with Fluent Ribbon
│   ├── Views/
│   └── Styles/
│
├── /Core             # Core logic and services
│   ├── PdfHandler.cs
│   ├── OcrEngine.cs
│   ├── LicenseManager.cs
│   └── ClassificationEngine.cs
│
├── /Services         # Helpers and infrastructure services
│   ├── FileService.cs
│   └── ValidationService.cs
│
├── /Resources        # Tesseract data, styles, icons
│
└── PDFClassifierPro.csproj
```

---

## 🔐 Licensing System

- **Free license**: permanently enabled after installation.
- **Pro license**: activated using a signed license key.
- License keys are verified locally and optionally validated against a remote API.
- Keys are signed using RSA 2048 or HMAC.
- Usage limits and status are stored securely in AppData (encrypted).

### License types

| Type | Details |
|------|---------|
| Trial | 7-14 days, full features |
| Free | Limited functionality, no expiry |
| Pro | Full features, bound to machine or account |
| Enterprise | Volume licensing, domain-bound |

---

## 📄 PDF Redaction Philosophy

PDF Classifier Pro supports two levels of redaction:
1. **Visual redaction**: overlays black bars to hide information visually.
2. **True redaction**: removes underlying text, ensuring content cannot be extracted.

Scanned documents are automatically converted via OCR into searchable, redactable formats.

---

## 🔄 Roadmap

- [x] Display PDFs via PdfiumViewer
- [x] Draw redaction areas on PDF
- [x] Integrate OCR with Tesseract
- [ ] Export redacted PDFs (true redaction)
- [ ] Implement licensing system (Portable.Licensing or custom RSA)
- [ ] Build Pro/Free feature separation
- [ ] UI polish (dark mode, animations)
- [ ] Add AI-based sensitive text detection
- [ ] Cross-platform port via Avalonia (v2+)

---

## 📦 Dependencies & Licenses

All third-party dependencies are compatible with **commercial redistribution**.

| Library | License | Purpose |
|--------|---------|---------|
| PdfiumViewer | BSD-3 | Fast PDF rendering |
| PdfPig | MIT | PDF text extraction |
| Tesseract OCR | Apache 2.0 | OCR recognition |
| Fluent.Ribbon | MIT | Modern Fluent UI |
| Portable.Licensing | MIT | License validation |

---

## 🧪 Development Setup

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022+
- Windows 10 or 11
- Tesseract (with language data in `/Resources/tessdata`)

### Getting Started

```bash
git clone https://github.com/nodasys/pdf-classifier-pro.git
cd pdf-classifier-pro
dotnet build
dotnet run
```

---

## 📜 License

This project is © 2025 [Nodasys](https://nodasys.com). All rights reserved.

---

## 🤝 Contact

Created and maintained by **Kevin GREGOIRE** at **Nodasys**.  
For enterprise inquiries, partnerships or licensing: [kevin.gregoire@nodasys.com](mailto:kevin.gregoire@nodasys.com)
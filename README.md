# PDF Classifier Pro

A professional Windows desktop application for PDF analysis, redaction, and classification with advanced OCR capabilities.

## 🚀 Features

### Core Functionality
- **PDF Text Extraction**: Extract and analyze text content from PDF documents
- **Document Classification**: Automatically classify documents based on content sensitivity
- **Redaction Engine**: Visual and true text removal redaction capabilities
- **OCR Integration**: Automatic OCR for scanned documents using Tesseract
- **License Management**: Free and Pro version with feature restrictions

### Pro Features (Requires License)
- **Advanced OCR**: Automatic text recognition for scanned documents
- **Unlimited Redaction**: No limits on redaction areas
- **Export Redacted PDFs**: True text removal with secure export
- **Advanced Classification**: Enhanced document classification algorithms
- **Secure Vault Export**: Encrypted export capabilities

## 🏗️ Architecture

### Project Structure
```
PDFClassifierPro/
├── PDFClassifierPro.Core/           # Core business logic
│   ├── Ocr/                        # OCR functionality
│   │   └── OcrEngine.cs
│   ├── Redaction/                  # Redaction engine
│   │   └── RedactionEngine.cs
│   ├── Classification/             # Document classification
│   │   └── ClassificationEngine.cs
│   ├── License/                    # License management
│   │   └── LicenseManager.cs
│   └── Utils/                      # Utility classes
│       ├── PdfHandler.cs
│       ├── PdfInspector.cs
│       └── FileService.cs
├── PDFClassifierPro.UI/            # WPF User Interface
│   ├── MainWindow.xaml             # Main application window
│   ├── Views/                      # UI components
│   │   ├── Controls/
│   │   │   └── PdfViewerControl.xaml
│   │   └── LicenseActivationDialog.xaml
│   └── App.xaml                    # Application entry point
└── PDFClassifierPro.Tests/         # Unit tests
    ├── Ocr/
    ├── Redaction/
    ├── Classification/
    ├── License/
    └── Utils/
```

### Technology Stack
- **.NET 8**: Modern C# framework
- **WPF**: Windows Presentation Foundation for UI
- **Fluent.Ribbon**: Professional Office-style ribbon interface
- **PdfiumViewer**: PDF rendering and manipulation
- **Tesseract**: OCR engine for text recognition
- **xUnit**: Unit testing framework

## 🛠️ Installation & Setup

### Prerequisites
- Windows 10/11
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code

### Build Instructions
```bash
# Clone the repository
git clone https://github.com/your-username/pdf-classifier-pro.git
cd pdf-classifier-pro

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run tests
dotnet test

# Run the application
dotnet run --project PDFClassifierPro.UI
```

## 📖 Usage

### Getting Started
1. **Launch the Application**: Run `PDFClassifierPro.UI.exe`
2. **Open a PDF**: Click "Open PDF" in the File group
3. **Analyze Content**: Use "Classify Document" to analyze sensitivity
4. **Add Redactions**: Use "Add Redaction Area" to mark sensitive content
5. **Export Results**: Save or export redacted documents

### License Activation
1. Click "Activate Pro" in the Pro Features group
2. Enter your Pro license key
3. Enjoy advanced features like unlimited redactions and OCR

### Document Classification Levels
- **Unclassified**: Public documents
- **Confidential**: Internal use only
- **Secret**: Sensitive information
- **Top Secret**: Highly classified content

## 🧪 Testing

The project includes comprehensive unit tests with 94% pass rate:

```bash
# Run all tests
dotnet test

# Run specific test categories
dotnet test --filter "Category=Ocr"
dotnet test --filter "Category=Redaction"
dotnet test --filter "Category=Classification"
```

### Test Coverage
- **OCR Engine**: Image processing and text extraction
- **Redaction Engine**: Visual and true redaction capabilities
- **Classification Engine**: Document sensitivity analysis
- **License Management**: Pro feature validation
- **Utility Classes**: File operations and PDF inspection

## 🔧 Development

### Code Organization
- **Single Responsibility**: Each class has one clear purpose
- **Modular Design**: Features are separated into logical modules
- **Clean Architecture**: Clear separation between UI, business logic, and data
- **No Comments**: Following project guidelines for clean code

### Adding New Features
1. Create feature class in appropriate Core subfolder
2. Add corresponding test class in Tests subfolder
3. Update UI if needed
4. Ensure all tests pass

### Build Configuration
- **Debug**: Development with full debugging
- **Release**: Optimized production build
- **Test**: Automated testing with coverage

## 📋 Requirements

### System Requirements
- **OS**: Windows 10/11 (x64)
- **RAM**: 4GB minimum, 8GB recommended
- **Storage**: 500MB available space
- **.NET**: .NET 8.0 Runtime

### Development Requirements
- **IDE**: Visual Studio 2022 or VS Code
- **SDK**: .NET 8.0 SDK
- **Git**: Version control

## 🚀 Deployment

### Release Build
```bash
# Create release build
dotnet publish PDFClassifierPro.UI -c Release -r win-x64 --self-contained

# Output location
PDFClassifierPro.UI/bin/Release/net8.0-windows/win-x64/publish/
```

### Installation Package
- Use WiX Toolset or similar for MSI creation
- Include .NET 8.0 Runtime if not self-contained
- Register file associations for .pdf files

## 🤝 Contributing

1. Fork the repository
2. Create feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open Pull Request

### Development Guidelines
- Follow existing code structure
- Add unit tests for new features
- Ensure all tests pass before submitting
- Update documentation as needed

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

### Common Issues
1. **OCR Not Working**: Ensure Tesseract data files are available
2. **PDF Loading Errors**: Check file permissions and PDF validity
3. **License Issues**: Verify license key format and validation

### Getting Help
- **Issues**: Create GitHub issue with detailed description
- **Documentation**: Check inline code comments and test examples
- **Community**: Join our Discord server for real-time support

## 🗺️ Roadmap

### Version 1.1
- [ ] Batch processing capabilities
- [ ] Advanced OCR with multiple languages
- [ ] Cloud storage integration
- [ ] Enhanced security features

### Version 1.2
- [ ] Machine learning classification
- [ ] API for enterprise integration
- [ ] Mobile companion app
- [ ] Advanced redaction tools

### Version 2.0
- [ ] Web-based interface
- [ ] Multi-platform support
- [ ] Enterprise deployment tools
- [ ] Advanced analytics dashboard

---

**PDF Classifier Pro** - Professional PDF analysis and redaction for the modern workplace.
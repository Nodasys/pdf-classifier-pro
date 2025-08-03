# Contributing to PDF Classifier Pro

Thank you for your interest in contributing to **PDF Classifier Pro** by Nodasys!

## 🙌 Who Can Contribute?

While this is a proprietary project, we welcome:
- Internal contributors within Nodasys.
- Selected external partners under signed NDA agreements.
- Feedback from testers and early adopters.

## 🛠️ Code Style

- Use `C# 10+` syntax (targeting .NET 8)
- Follow the [Nodasys internal C# coding standards](https://nodasys.com/dev-guidelines)
- Use `PascalCase` for classes, `camelCase` for variables
- Place UI logic in `/UI`, business logic in `/Core`

## 🔧 Development

1. Clone the repository:
   ```bash
   git clone https://github.com/nodasys/pdf-classifier-pro.git
   ```
2. Install prerequisites (.NET 8 SDK, Visual Studio 2022)
3. Keep functionality modular and testable

## 🧪 Testing

- Use unit tests via xUnit or MSTest
- Place tests under `/Tests` with meaningful method names
- All public methods must be tested

## 🧾 Commit Guidelines

Follow this format:

```
[type] Short summary

Longer description if needed.

Issue: #123 (if applicable)
```

Examples:
- `[fix] Corrects OCR issue on rotated scans`
- `[feat] Adds Pro license key validation`
- `[docs] Updates README with feature roadmap`

## 🛡️ Legal & Licensing

Contributors agree to assign all rights of their contributions to Nodasys.
Open-source contributions must respect the licenses of included libraries.

---

For any questions or suggestions, contact us at: kevin.gregoire@nodasys.com
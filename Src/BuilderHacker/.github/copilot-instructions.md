# Copilot Instructions

## Project Guidelines
- Support multiple .NET frameworks: .NET Framework, .NET Core 2.0-5.0, and .NET 6.0+. Key compatibility requirements:
1. Avoid .NET 7+ LINQ methods (DistinctBy, etc.)
2. Avoid nullable reference types syntax for Framework/Core 2-3
3. Use conditional compilation for framework-specific features
4. Avoid implicit usings in older frameworks
5. Ensure Roslyn APIs work across all versions
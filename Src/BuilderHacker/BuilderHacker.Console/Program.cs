using BuilderHacker.Core.HtmlBuilder;
using System;
using System.Diagnostics;

namespace BuilderHacker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("BuilderHacker HTML Builder Performance Benchmark");
            System.Console.WriteLine("================================================\n");

            // Warm up
            var tree = BuildSampleTree();
            _ = tree.Render();

            const int iterations = 10_000;

            System.Console.WriteLine($"Rendering {iterations:N0} times...\n");

            var sw = Stopwatch.StartNew();
            long totalLength = 0;

            for (int i = 0; i < iterations; i++)
            {
                var html = tree.Render();
                totalLength += html.Length;
            }

            sw.Stop();

            var totalMs = sw.Elapsed.TotalMilliseconds;
            var opsPerSecond = iterations / (totalMs / 1000);
            var avgMs = totalMs / iterations;

            System.Console.WriteLine($"Total time:        {totalMs:F2} ms");
            System.Console.WriteLine($"Per iteration:     {avgMs:F4} ms");
            System.Console.WriteLine($"Ops/second:        {opsPerSecond:F0}");
            System.Console.WriteLine($"Total chars:       {totalLength:N0}");
            System.Console.WriteLine($"Avg bytes/op:      {totalLength / iterations:N0}");
        }

        static BuilderHacker.Abstraction.HtmlBuilder.IHtmlNode BuildSampleTree()
        {
            // Build a realistic HTML page with a form and table
            return UI.HtmlDocument(
                UI.Head(
                    UI.Title("Sample Page"),
                    UI.Meta("viewport", "width=device-width, initial-scale=1")
                ),
                UI.Body(
                    UI.Header(
                        UI.Heading(Heading.H1, UI.TextNode("Welcome")),
                        UI.Nav(UI.TextNode("Navigation"))
                    ),
                    UI.Main(
                        UI.Form(
                            UI.Label("Name:"),
                            UI.Input("text", "John Doe"),
                            UI.Label("Email:"),
                            UI.Input("email", "john@example.com"),
                            UI.Button("Submit")
                        ),
                        UI.Section(
                            UI.Heading(Heading.H2, UI.TextNode("Data Table")),
                            UI.Table(
                                UI.THead(
                                    UI.Tr(
                                        UI.Th("ID"),
                                        UI.Th("Name"),
                                        UI.Th("Status")
                                    )
                                ),
                                UI.TBody(
                                    BuildRows(10)
                                )
                            )
                        )
                    ),
                    UI.Footer(
                        UI.P(UI.TextNode("© 2026 BuilderHacker"))
                    )
                )
            );
        }

        static BuilderHacker.Abstraction.HtmlBuilder.ITableRow[] BuildRows(int count)
        {
            var rows = new BuilderHacker.Abstraction.HtmlBuilder.ITableRow[count];
            for (int i = 0; i < count; i++)
            {
                rows[i] = UI.Tr(
                    UI.Td($"{i + 1}"),
                    UI.Td($"Item {i + 1}"),
                    UI.Td(i % 2 == 0 ? "Active" : "Inactive")
                );
            }
            return rows;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditorExample.Helpers {
    internal static class Constants {
        public static string MyLangMonarch = @"{
  defaultToken: ""invalid"",

  keywords: [
    ""function"", ""let"", ""const"", ""if"", ""else"",
    ""return"", ""while"", ""for"", ""true"", ""false"", ""null""
  ],

  typeKeywords: [""number"", ""string"", ""boolean""],

  operators: [
    ""="", "">"", ""<"", ""!"", ""~"", ""?"", "":"", ""=="", ""<="",
    "">="", ""!="", ""&&"", ""||"", ""+"", ""-"", ""*"", ""/"", ""%""
  ],

  symbols: /[=><!~?:&|+\-*\/%]+/,

  escapes: /\\(?:[abfnrtv\\""'`]|x[0-9A-Fa-f]{2}|u[0-9A-Fa-f]{4})/,

  tokenizer: {

    root: [

      // identifiers
      [/[a-zA-Z_$][\w$]*/, {
        cases: {
          ""@keywords"": ""keyword"",
          ""@typeKeywords"": ""type"",
          ""@default"": ""identifier""
        }
      }],

      // whitespace
      { include: ""@whitespace"" },

      // delimiters
      [/[{}()\[\]]/, ""@brackets""],

      // operators
      [/@symbols/, {
        cases: {
          ""@operators"": ""operator"",
          ""@default"": """"
        }
      }],

      // numbers
      [/\d*\.\d+([eE][\-+]?\d+)?/, ""number.float""],
      [/\d+/, ""number""],

      // regex literal (complex rule object)
      {
        regex: /\/(?!\*)(?:[^\\/]|\\.)+\/[gimsuy]*/,
        action: { token: ""regexp"" }
      },

      // strings
      [/""/, { token: ""string.quote"", bracket: ""@open"", next: ""@string"" }],

      // template string
      [/`/, { token: ""string.quote"", bracket: ""@open"", next: ""@template"" }]
    ],

    comment: [
      [/[^/*]+/, ""comment""],
      [/\/\*/, ""comment"", ""@push""],
      [/\*\//, ""comment"", ""@pop""],    
      [/./, ""comment""]
    ],

    string: [
      [/[^\\""]+/, ""string""],
      [/@escapes/, ""string.escape""],
      [/\\./, ""string.escape.invalid""],
      [/""/, { token: ""string.quote"", bracket: ""@close"", next: ""@pop"" }]
    ],

    template: [
      [/[^\\`$]+/, ""string""],
      [/\$\{/, { token: ""delimiter.bracket"", next: ""@braced"" }],
      [/@escapes/, ""string.escape""],
      [/`/, { token: ""string.quote"", bracket: ""@close"", next: ""@pop"" }]
    ],

    braced: [
        [/}/, { token: ""delimiter.bracket"", next: ""@pop"" }],
        { include: ""@root"" }
    ],

    whitespace: [
      [/[ \t\r\n]+/, ""white""],
      [/\/\*/, ""comment"", ""@comment""],
      [/\/\/.*$/, ""comment""]
    ]
  }
}";

        public const string MyLangConfiguration = @"{
  comments: {
    lineComment: ""//"",
    blockComment: [""/*"", ""*/""]
  },
  brackets: [
    [""{"", ""}""],
    [""["", ""]""],
    [""("", "")""]
  ],
  autoClosingPairs: [
    { open: ""{"", close: ""}"" },
    { open: ""["", close: ""]"" },
    { open: ""("", close: "")"" },
    { open: ""\"""", close: ""\"""" },
    { open: ""`"", close: ""`"" }
  ],
  surroundingPairs: [
    { open: ""{"", close: ""}"" },
    { open: ""["", close: ""]"" },
    { open: ""("", close: "")"" },
    { open: ""\"""", close: ""\"""" },
    { open: ""`"", close: ""`"" }
  ]
}";

        public const string TestText = @"
function test(x: number) {

    /* outer comment
        /* nested comment */
    */

    let value = 10.5
    const flag = true

    let regex = /abc\d+/gi

    let str = ""hello world""

    let template = `value is ${value}`

    if (flag && value > 5) {
        return template
    }

    return null
}";

        public const string defaultCSharpText = @"/*
* C# Program to Display All the Prime Numbers Between 1 to 100
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isPrime = true;
            Console.WriteLine(""Prime Numbers : "");
            for (int i = 2; i <= 100; i++)
            {
                for (int j = 2; j <= 100; j++)
                {
                    if (i != j && i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    Console.Write(""\t"" +i);
                }
                isPrime = true;
            }
            Console.ReadKey();
        }
    }
}";
    }
}

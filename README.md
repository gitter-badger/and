<img src="http://i.hizliresim.com/YrjYYl.png" alt="And logo" height="110">
And Projesi
======

And, betik tabanlı(script oriented) programlama dili projesidir.

Bu projede temel hedefim;  
	1.[Parsing](https://en.wikipedia.org/wiki/Parsing)  
	2.[Interpreters](https://en.wikipedia.org/wiki/Interpreter_%28computing%29)  
	3.[Lexical analysis](https://en.wikipedia.org/wiki/Lexical_analysis)  
	4.[Semantic analysis](https://en.wikipedia.org/wiki/Semantic_analysis_%28compilers%29)  
	5.[Abstract Syntax Tree](https://en.wikipedia.org/wiki/Abstract_syntax_tree)

.. gibi konularda tecrübe edinebilmek ve ortaya somut bir çalışma koymaktır.

And projesi Syntax yapısını C ve Python gibi iki farklı seviyedeki dilin karışımından elde etmekte.  
Planlanan örnek bir kod betiği;

```go
# Main fonksiyonu, programın ana girişini tanımlar.
func main(args) {
    writeln("Hello, world!");
}
```

Durum [![](https://ci.appveyor.com/api/projects/status/bs3n424y3bk1ejg4?svg=true)](https://ci.appveyor.com/project/ertseyhan/and)
------

Lexical analyser aşamasından şuan memnunum. Özellikle son yaptığım değişiklikler ile beraber güzel bir performans kazancı elde ettim. Ortalama bir test süreci 10 milisaniye sürerken, 4 milisaniyelere kadar düştü. Lexer'in gelişimi zaman ile devam edecek. Şuan işin asıl önemli kısmına, Parser ve AST'ye geldik. Bu konu hakkında teorik olarak hazırlanıyorum. Normal kaynakların dışında modern tasarım desenlerini vs. inceliyorum.  

Teorik araştırma ve çalışmalarla beraber götürüyorum projeyi.  
Dolayısıyla yavaş ilerleyecektir.

İletişim
------

Herhangi bir soru, mesaj vs. için irc.ertugrulseyhan.com:6667 irc sunucusuna gelebilir veya hi@ertugrulseyhan.com adresine mail atarak benimle iletişime geçebilirsiniz.
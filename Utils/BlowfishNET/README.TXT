
This is an implementation of the Blowfish encryption algorithm in C#, running
on the Microsoft.NET platform. It offers ECB, CBC and CFB encryption and
has been speed-tuned to the maximum, offering one of the best block cipher
performances possible for safe code.

It comes with a framework of classes to offer you easy string encryption
(BlowfishSimple) and Java interoperability (BlowfishEasy and BlowfishStream),
for troublefree cross-platform data exchange on string or stream basis, working
together with the open-source BlowfishJ package.

The code has been tested very carefully, based on an extended set of test cases,
which you can run in the latest NUnit tool to prove it. Compatibility with the
OpenSSL Blowfish implementation in CFB mode has been tested (ECB/CBC should
work as well) positively.

For beginners is is recommended to try out the BlowfishDemo sample first, which
shows you the basics of the Blowfish.NET assembly. Simply open the solution in
Visual Studio 2008 (Express is supported). If you don't have NUnit install you
might want to remove the BlowfishTests porject for proper building.

Usage of Blowfish.NET is free for non-commerical usage (this includes student
and pure research projects, plus non-profit organizations). Notice that
Blowfish.NET is not distibuted under any open source license. Modifications,
unauthorized redistribution and partial usage of classes are stricly prohibited.

This software is provided "as is" and any expressed or implied warranties are
disclaimed. If you are interested to use the code in your commerical product
please contact me for license details.

The latest versions of Blowfish.NET and BlowfishJ can always be downloaded at
<http://www.hotpixel.net/software.html>. Please verify the MD5 checksums for
integrity.

Copyright 2001-2009 Markus Hahn <mhahn@cruzio.com>

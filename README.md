# Krypt2Library
A Polyalphabetic Cipher which uses a passphrase to generate a SHA256 hash from which 8 seeded Random's are generated. These, each in its own pass, are used to deterministically shift each character in sequence.

## Example 1:  

**Message**:  
Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.

**CipherText** *with passphrase "password"*:  
"tkcmGG8H.QFQ+5tSfMukINWowdVHIm7L?3U4t0Ok*+g9&'K9#X3x"1KA0:%jLz2R#G:LoXffPii(7OJA;wSJAR,SOlr@XA5#kFVWF8VGZ%(9rc+6T4sp6ZC5Mey08(DsCxOl#Tl% z+u:%+@(IMOe4qW#Koe1Z@DpU2AA""?2'1O!Fg3T4xtRxz.*OmY%(Ps7R,u4xYnXuwzbQ6 qLEJtVfaMWJQ"A-5tVbp,T?qvo;cJnksEOvN)ZYh1x?e%$zcGRoY+Q$8DR'p%CJRGAwOz6*S65FZkx5D0Dvl#pqrXSjA4 O-#Kt"8+$1%Nas+tZG$Xoj2F1u"pi"Hw$+C,9c&9X:zNB0ugB*(6EJ63BT#hr5gSm@+za6e"ECdjFx*9"k7gIay:pZQGbZL(wyVd0FdJe-LjUL'Lt*hGvi+frJ$UZ9BF*g:)!#Ow'F&YV,

**CipherText** *with passphrase "Password"*:  
+-AEc1!4:o3AcqRW #7OX)-9osD2WaV-ApTtR, YtIod@PBR2T,Oy9?nS5r)d?eRO'Q+,Aj8 0vlnxN(.kMf'&?9,3X-l,SMYMH?d9QBRle5PzM1"Mp;D$h3FW%IzRC)n6XK44PqFo5'.tlk00l oXx3KVbRSv7xI Rfg,&k9jaGBGsCWvfqw7l4R'Tc'ULCS8D76L1$!-XO3@maZL(l3J#$4cvz)hIwWUa!#2E*SDHS:gmcMnSr66z8.ZO7oN!7CM)rg!h@pZ&%nXGRfn#;6M.fqb!owdyRVM7bI@1YM4ef5s$O)e?U9dpj3p:k%9SoYSNyG-psTcPsMNaelI1wV10RNQsDtT ?8"Ljg9F$)5h%1lbhJsv1Y?"(ebVL;87'YVJmCuzz+?V AZ;:bFS*7i-e%1kw+NM"7h0B!519t,)uBVoPf.qWpn%w3AI78

## Example 2:  

**Message**:  
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

**CipherText** *with passphrase "password"*:  
Ef#+aZyTpREYN?UfByum)1NKkdu!Fu-PH.KQL-J7k$()17!CW''Zm7I1TI?@CIl@N!mZzaUy"L&#5Q7Bn'oPBxxZz7

**CipherText** *with passphrase "Password"*:  
Q!jA()6PZ$RT-cGITmPGEk-Xk-U@U"I wnApyST:tFg:?xzJPNcKn1UE.Nn:w ( K8w90mg+RWj&-g6.YiEc8#TWT#

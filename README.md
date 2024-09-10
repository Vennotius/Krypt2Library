# Krypt2Library
A Polyalphabetic Cipher which uses a passphrase to generate a SHA256 hash from which 8 seeded Random's are generated. These, each in its own pass, are used to deterministically calculate the final shift value of each character in sequence.

# General Information

## Alphabet Extension

A standard alphabet is used. When encrypting a plaintext message, if characters are discovered that are not contained in the standard alphabet, they are added to it. Those characters are then sorted and prepended to the resulting CipherText. On decryption they can be read and the alphabet that was used for encryption is thereby reconstructed.

## Use of SHA and a Pseudo Random Number Generator

Using a passphrase to determine the shift amount of each character was considered, but the main problems are firstly that the shift values will repeat because the passphrase is likely to be much shorter than the message, and secondly that a partially correct passphrase of the correct length could yield partial decryption. For this reason a PRNG is used. A seeded PRNG can extend indefinitely and is deterministinc, meaning that one can reproduce the exact pseudo-random sequence using the same seed. We hash the passphrase using SHA512, using the result as a seed for a Xoshiro PRNG. One tiny change to the passphrase and the seed changes drastically, meaning a completely different sequence of shift amounts.

# Examples

## English Text:  

**Message**:  
>Therefore God has highly exalted him and bestowed on him the name that is above every name, so that at the name of Jesus every knee should bow, in heaven and on earth and under the earth, and every tongue confess that Jesus Christ is Lord, to the glory of God the Father.


**CipherText** *with passphrase "password"*:  
```
RN&w8RInWfV4#pTC5xD6TCe?JLo%J)9GV-GnTB!sC%7VlowNj7IF%DBa2a8T;2yb48IoUcbi&%UyGj%7QRjS0oppWb($eu; .X04 U$%;klUi&FeBBIx(*WdRD8rfVH+4:aiTkyQBlI+7JsQMevD%ah8x-Cx6j1u'j"n;,3h#AkUEdOlKI7Ql6iH H;q7ypG1G1?(GJ4*GBZQ@xBcCYpM GToGst U:*&A83&Xbu7"zh1$V;?baS$Q3Q4GP3SaLTJSw7hyY 6jysO-Y
```

**CipherText** *with passphrase "Password"*:  
```
ZEt3(:6bpUDW,F m.1)ntN1sR8Vko7cWn8;YmjvEta;J.!$Z3,(Lr"M7%IQR'xOxMUN1x?Hne1LUy:(4XKy!sIJ51V'Q&DfdJRA!-j1pR6OWibtYArZ%FR7otdtIG1f#tUo5R  !$)5ehQB+a#bbZwm"EHV8A'Wvpn(bG#x$7-:aGrW.9uhBJ,y32vTB)z!7KoIhN1UZd!YVoBX0gT!+&h9lX75'+W* qyj$@Gw8Ulk3jT32""+e3,!SoJD&vX2uLlcT3$Wl!vSpOZ;
```

## Repeating Character:  

**Message**:  
> aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa


**CipherText** *with passphrase "password"*:  
```
+G@f4Mu&SypQ(IMCNQwYNv#P2H)%y15D!@yb,B1pV$3D@aaJg*usowt:@ 1PiPy;0+phU,ua7oUxs:)*MwfBCHcpK*
```

**CipherText** *with passphrase "Password"*:  
```
gxpM",S?l?*I9Y3mT)?fnGQ$ 4ykdO+TG1 MFjiBM-!rS01V0c5yK8EVopJNgkOlI?uUxU0f")LTkW"$TpuX$1w5PR
```


# How secure is this cipher?

I am not an expert, but I do think that it is secure. If anyone wants to prove that they can break the cipher, I would welcome a successful attempt, as it would be an opportunity to improve and to learn something of value.

# Console Application

See [this repository](https://github.com/Vennotius/KryptConsole) for a console application that uses this library.

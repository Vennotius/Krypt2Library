# Krypt2Library
A Polyalphabetic Cipher which uses a passphrase to generate a SHA256 hash from which 8 seeded Random's are generated. These, each in its own pass, are used to deterministically calculate the final shift value of each character in sequence.

# General Information

## Use of TextElements instead of Characters

Certain Unicode characters are composed of more than one character. Because of this, if one iterates through characters, swapping them as one goes, one could end up splitting up characters which then causes problems when saving result to file for example. Therefore, this cipher iterates on TextElements instead.

See https://docs.microsoft.com/en-us/dotnet/api/system.globalization.stringinfo.gettextelementenumerator?view=net-6.0 for more information. 

## Alphabet Extension

A standard alphabet is used. When encrypting a plaintext message, if characters are discovered that are not contained in the standard alphabet, they are added to it. Those characters are then sorted and prepended to the resulting CipherText. On decryption they can be read and the alphabet that was used for encryption is thereby reconstructed.

## Use of SHA & Randoms

Using a passphrase to determine the shift amount of each character was considered, but the main problems are firstly that the shift values will repeat because the passphrase is likely to be much shorter than the message, and secondly that a partially correct passphrase of the correct length could yield partial decryption. For this reason Random.cs is used. A seeded Random can extend indefinitely and is deterministinc, meaning that one can reproduce the exact pseudo-random sequence using the same seed. The seed used for Random.cs is an Int32. This means that a SHA256 hash can be used to construct 8x Int32 seeds. One tiny change to the passphrase and the seeds change drastically.

Why 8x randoms instead of just one? Because using just 32bits out of the 256 bit hash yields hash collisions much too often. Using 8 randoms, means taking advantage of the full hash, and every random contributes to each character's final shift amount.

# Examples

## English Text:  

**Message**:  
> Therefore God has highly exalted him and bestowed on him the name that is above every name, so that at the name of Jesus every knee should bow, in heaven and on earth and under the earth, and every tongue confess that Jesus Christ is Lord, to the glory of God the Father.

**CipherText** *with passphrase "password"*:  
> $m*pe4M ty ,QU1fTfBu*8Y8)hR!QNdSo;S2s-W )%$j)@hGZXb,#!Q?A1)&jVleRVF6ztBGes&$(..iris6ZeKZL.38D0h$"#UVSF8.zSR"5W$8!LL)t?.EsQRVx?(JsypOD**t+0+'1;3V@dd .:5mR#xn;pW!hMQWPqU775E?N42dz1.p+HQFbdj5Q(mjo Ntys9,pVyg+:$"SNZWJKO#hf-z+eEW"wW;H,Slf.R!MM51H.nxA7C$p1D$XC)#1DQ90bN0*A66l#D

**CipherText** *with passphrase "Password"*:  
> g@nR$&)634$7c,NI.#WOKrk@)d:@5fM?duIBfS6)aGkgdLVNSuqX$82QS6u@d$(#OPP'0F'g9Dj*nB S2DItd36W5*mQW"z4RvW?-9QRKe01L4EI$E&RHbt5@0I5&WC*n2PK#8#yIeE5pu(70?9fLHyZFV:QD,4qmeN-v24c3mRSAwPzsDmibXE  #oV8P7&O.zo -ki'*1yC8TjH8eD30"..Y1pCzM(6Xb2l2DyH( Oyj4T1UrtTTce(ZU:7).B+J(,i:eYHWl!jVH

## Repeating Character:  

**Message**:  
> aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

**CipherText** *with passphrase "password"*:  
> Ef#+aZyTpREYN?UfByum)1NKkdu!Fu-PH.KQL-J7k$()17!CW''Zm7I1TI?@CIl@N!mZzaUy"L&#5Q7Bn'oPBxxZz7

**CipherText** *with passphrase "Password"*:  
> Q!jA()6PZ$RT-cGITmPGEk-Xk-U@U"I wnApyST:tFg:?xzJPNcKn1UE.Nn:w ( K8w90mg+RWj&-g6.YiEc8#TWT#


# How secure is this cipher?

I am not an expert, but I do think that it is secure. If anyone wants to prove that they can break the cipher, I would welcome a successful attempt, as it would be an opportunity to improve and to learn something of value.

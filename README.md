# Krypt2Library
A Polyalphabetic Cipher which uses a passphrase to generate a SHA256 hash from which 8 seeded Random's are generated. These, each in its own pass, are used to deterministically shift each character in sequence.

## Example 1:  

**Message**:  
Therefore God has highly exalted him and bestowed on him the name that is above every name, so that at the name of Jesus every knee should bow, in heaven and on earth and under the earth, and every tongue confess that Jesus Christ is Lord, to the glory of God the Father.

**CipherText** *with passphrase "password"*:  
$m*pe4M ty ,QU1fTfBu*8Y8)hR!QNdSo;S2s-W )%$j)@hGZXb,#!Q?A1)&jVleRVF6ztBGes&$(..iris6ZeKZL.38D0h$"#UVSF8.zSR"5W$8!LL)t?.EsQRVx?(JsypOD**t+0+'1;3V@dd .:5mR#xn;pW!hMQWPqU775E?N42dz1.p+HQFbdj5Q(mjo Ntys9,pVyg+:$"SNZWJKO#hf-z+eEW"wW;H,Slf.R!MM51H.nxA7C$p1D$XC)#1DQ90bN0*A66l#D

**CipherText** *with passphrase "Password"*:  
g@nR$&)634$7c,NI.#WOKrk@)d:@5fM?duIBfS6)aGkgdLVNSuqX$82QS6u@d$(#OPP'0F'g9Dj*nB S2DItd36W5*mQW"z4RvW?-9QRKe01L4EI$E&RHbt5@0I5&WC*n2PK#8#yIeE5pu(70?9fLHyZFV:QD,4qmeN-v24c3mRSAwPzsDmibXE  #oV8P7&O.zo -ki'*1yC8TjH8eD30"..Y1pCzM(6Xb2l2DyH( Oyj4T1UrtTTce(ZU:7).B+J(,i:eYHWl!jVH

## Example 2:  

**Message**:  
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

**CipherText** *with passphrase "password"*:  
Ef#+aZyTpREYN?UfByum)1NKkdu!Fu-PH.KQL-J7k$()17!CW''Zm7I1TI?@CIl@N!mZzaUy"L&#5Q7Bn'oPBxxZz7

**CipherText** *with passphrase "Password"*:  
Q!jA()6PZ$RT-cGITmPGEk-Xk-U@U"I wnApyST:tFg:?xzJPNcKn1UE.Nn:w ( K8w90mg+RWj&-g6.YiEc8#TWT#

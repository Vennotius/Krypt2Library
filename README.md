# Krypt2Library
A Polyalphabetic Cipher which uses a passphrase to generate a SHA256 hash from which 8 seeded Random's are generated. Theses, each in its own pass, are used to deterministically generate a new alphabet per character.

## Example 1:  

**Message**:  
"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."

**CipherText** *with passphrase "password"*:  
"hsq2ePltZ@,J\"-,Xej9T$KPNn0Hc.(,vBv rnwC+n11)tEv(VyMukmHkvr@uhBtbB#a\"bmcj;.5vUOjW7rS8f.D2!K6jh4ckk2,C%RsDJC7S\")lVhyw.V2O$$X.TRxqg6*k)lVF1aabz&b!R!\"tlMSq@%kd%p8)zKy)yU-rAl0+%-DWOt!F@B0)PanPO7;kJh4?B?@z7leS !\"(&4xDK9Od,Vgq;KNeoawk+O5KZ6ilbd9kCa; )yyf.yZBKVz1'qumThAj\"k'pqYvZBY$RE;)c3H3A\"e2ksii?.7TDs);#7JzI0Ta8*U'?)e*7')aMGhAo#fsm!eO7o8Bsr5p7YU69.%hFbXwTi1I!RKoix!Ta*GBVxk9ozN0(.ua1g(a#'0%cN,47!ZV,dKiC83dJqI8CQ%7)6FW.a(K+9KQfMU!\"vfL1MTc7\"G i\"@3x0L"

**CipherText** *with passphrase "Password"*:  
"zn;\"aHE.\"Od8TVBk.N.+L@;:Kju0)zHAve\"-t)ZYrfsDv(spur&U0Qx56X\"O&qJ\"F2HULFBG%e@vhzQE-BEhX$I4(O#X4WLP..702lpcxD:@0oK63x15wjDNVJN)l93xV4hRQyn4tb#!uJobm;L 4 5rrcFmR-?w5JVz 3@(2xlMBsKG6;?psRtJUD5@VhuF*V@7mnczr%H?C&!0HthZw)f4IPIAfGJbw!GlPVt1(6*-(zSy9fjjGp6D*LyuX3Ps9v6V73(N.G9aMOKAk\"VsGi#6ZWf8w1R#+S#NuoYksPy&#2)T3Cnp!ZVF3ST?!7!NYw-Vlls&$5wBGZiyhc1Y6t8iEqwOaBzTrHjtd)35b8FQ8+#JDYEuVrpW#SctGD,3SdKV?8UsuJ,5-m+kh)PF-*qHwA!9a)dM&:Rt$ku+$KPBrGS#6UWINq ;HW+ez"

## Example 2:  

**Message**:  
"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"

**CipherText** *with passphrase "password"*:  
"4(QCi7R0CB0P8pJENiKzJRP#a)TZkU@G!*ZNdosRn'qz 5rvwXkkS6G po7RFOCyI.Gbe0VPmuYN8EWbAdbd9,zy4#"

**CipherText** *with passphrase "Password"*:  
"OU\"qrZlAqG&x46+uuLp9oO;W'+6us&GJjNb+IQ6OrD1a(0Hq4zoXQ.Q*qcJ9(; uYtDs$DIcq*M;+BkVs+,x b$M56"

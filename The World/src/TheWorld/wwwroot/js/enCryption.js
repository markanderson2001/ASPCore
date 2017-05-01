//Encrypt decrypts for test
Imports java.io.inputstream;
Imports java.io.outputstream;
Imports javax.crypto.Cipher;
Imports javax.crypto.CipherINputsteream
Imports javax.crypto.CipherOutputstream
Imports javax.crypto.SecretKey;
Imports javax.crypto.SecretKeyFactory;
Imports javax.crypto.Spec.DESKeySpec;


Public class EncryptDecrypt{
    Protected static String key =  "markandesone93847823B";
	
	Public static void encrypt(string key, InputStream is,OutputStream os) Throwable {
	    encryptOrDecrypt(key,Cipher.ENCRYPT_MODE,is,os};
	
        Public static void decrype(string key, InputStream is,OutputStream os) Throwable {
	encryptOrDecrypt(key,Cipher.DECRYPT_MODE,is,os};
	
    Public static void encryptORDecrypt (String key,intmode,InputStream is,OutputStream os)Throwable 
	
        DESKeySpec dks = new DESKeySpec(key.getBytes());
        SecretKeyFactory skf = SecretKeyFactory.getInstance("DES" );
        SecretKey deKey = skf.generateSecrret(dks);
        Cipher cipher = Cipher.getInstance("DES); //DES/ECBPKCS5Padding for SunJoe
	
        If (mode = =Cipher.ENCRYPT_MODE)
	Cipher.init

# iviz_xmlrpc

iviz_xmlrpc is a partial implementation of the XML-RPC API, for use in iviz_roslib.

The difference to libraries like XML-RPC.NET is that we do not employ dynamic code generation (e.g., _Emit_ instructions), making it suitable for AOT compilations. 

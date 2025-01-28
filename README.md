# copc

Reader for COPC files in .NET 8.0. No dependencies.

## Specs 

Spec: https://copc.io/

Spec: https://www.asprs.org/wp-content/uploads/2019/07/LAS_1_4_r15.pdf


## Validators

Validator: https://validate.copc.io/

## Samples

Sample file: https://s3.amazonaws.com/hobu-lidar/sofi.copc.laz

## Implementations 

- https://github.com/DaviesCooper/LASReader

- https://github.com/pka/copc-rs/

- https://github.com/hobuinc/copcverify

## Sample code

```csharp
var url = "https://s3.amazonaws.com/hobu-lidar/sofi.copc.laz";
var httpClient = new HttpClient();
var headerBytes = await GetHttpRange(url, httpClient, 0, 589);
var reader = new BinaryReader(new MemoryStream(headerBytes));
var copc = CopcReader.Read(reader);
```

## Status 2025-01-29

Done: Reading COPC Header, COPCInfo, VlrInfo

Todo: Reading Vlr's + Evlrs + Points + Chunks _

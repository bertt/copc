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

- C#: https://github.com/DaviesCooper/LASReader

- Rust: https://github.com/pka/copc-rs/

- Javascript: https://github.com/connormanning/copc.js

- C++: https://github.com/hobuinc/copcverify

## Sample code

Read COPC from URL:


```csharp
var httpClient = new HttpClient();
var url = "https://s3.amazonaws.com/hobu-lidar/sofi.copc.laz";
var copc = await CopcReader.Read(httpClient, url);
```

Read COPC from file:

```csharp
var file "./testfixtures/ellipsoid.copc.laz"
var copc = await CopcReader.Read(file);
```

## Status 2025-02-11

Done: Reading COPC Header, COPCInfo, Vlrs, eVLRS

Todo: Reading Points + Chunks + LasZip

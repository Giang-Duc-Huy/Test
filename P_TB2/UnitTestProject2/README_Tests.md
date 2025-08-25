# Unit Tests cho Phương trình Bậc 2 - UnitTestProject2

## Tổng quan

Bộ unit test này được thiết kế để kiểm tra toàn diện chức năng giải phương trình bậc 2 trong dự án P_TB2. Các test được tổ chức thành 2 file chính:

1. **UnitTest1.cs** - Các test cơ bản và tích hợp
2. **QuadraticEquationAdvancedTests.cs** - Các test nâng cao và stress test

## Cấu trúc Test

### 1. UnitTest1.cs - Basic Tests

#### Test QuadraticEquation Model
- **TestQuadraticEquation_Constructor**: Kiểm tra constructor
- **TestQuadraticEquation_Solve_TwoDistinctRoots**: Phương trình có 2 nghiệm phân biệt
- **TestQuadraticEquation_Solve_OneRoot**: Phương trình có nghiệm kép
- **TestQuadraticEquation_Solve_NoRealRoots**: Phương trình vô nghiệm
- **TestQuadraticEquation_Solve_LinearEquation**: Phương trình bậc 1
- **TestQuadraticEquation_Solve_ConstantZero**: Phương trình có vô số nghiệm
- **TestQuadraticEquation_Solve_ConstantNonZero**: Phương trình vô nghiệm (hằng số)
- **TestQuadraticEquation_Solve_ComplexRoots**: Phương trình có nghiệm phức
- **TestQuadraticEquation_IsValidInput**: Kiểm tra tính hợp lệ của input

#### Test QuadraticController
- **TestQuadraticController_SolveEquation_***: Các test cho phương thức SolveEquation
- **TestQuadraticController_ValidateInput_***: Các test cho phương thức ValidateInput
- **TestQuadraticController_ClearResult**: Test phương thức ClearResult

#### Test Edge Cases
- **TestQuadraticEquation_Solve_VeryLargeNumbers**: Test với số rất lớn
- **TestQuadraticEquation_Solve_VerySmallNumbers**: Test với số rất nhỏ
- **TestQuadraticEquation_Solve_NegativeCoefficients**: Test với hệ số âm
- **TestQuadraticEquation_Solve_ZeroBAndC**: Test với B và C bằng 0

#### Test Integration
- **TestIntegration_CompleteWorkflow**: Test quy trình hoàn chỉnh
- **TestIntegration_InvalidInputWorkflow**: Test quy trình với input không hợp lệ

#### Test Mathematical Accuracy
- **TestMathematicalAccuracy_PerfectSquare**: Test độ chính xác với bình phương hoàn hảo
- **TestMathematicalAccuracy_StandardForm**: Test độ chính xác với dạng chuẩn
- **TestMathematicalAccuracy_DecimalCoefficients**: Test với hệ số thập phân

#### Test Input Validation
- **TestInputValidation_VariousFormats**: Test các định dạng input khác nhau
- **TestInputValidation_BoundaryValues**: Test giá trị biên

### 2. QuadraticEquationAdvancedTests.cs - Advanced Tests

#### Performance Tests
- **TestPerformance_MultipleEquations**: Test hiệu suất với nhiều phương trình
- **TestPerformance_ControllerMethods**: Test hiệu suất các phương thức controller

#### Stress Tests
- **TestStress_LargeCoefficients**: Test với hệ số rất lớn
- **TestStress_SmallCoefficients**: Test với hệ số rất nhỏ

#### Exception Handling Tests
- **TestExceptionHandling_ControllerSolveEquation**: Test xử lý ngoại lệ
- **TestExceptionHandling_InvalidMathematicalOperations**: Test với phép toán không hợp lệ

#### Memory Tests
- **TestMemoryUsage_MultipleInstances**: Test sử dụng bộ nhớ

#### Concurrency Tests
- **TestConcurrency_MultipleControllers**: Test đồng thời

#### Regression Tests
- **TestRegression_KnownWorkingCases**: Test hồi quy với các trường hợp đã biết

#### Special Cases Tests
- **TestSpecialCase_ZeroAAndB**: Test với A và B bằng 0
- **TestSpecialCase_ZeroAAndC**: Test với A và C bằng 0
- **TestSpecialCase_ZeroBAndC**: Test với B và C bằng 0
- **TestSpecialCase_AllZeros**: Test với tất cả hệ số bằng 0

#### Mathematical Edge Cases
- **TestMathematicalEdgeCase_VerySmallDelta**: Test với delta rất nhỏ
- **TestMathematicalEdgeCase_VeryLargeDelta**: Test với delta rất lớn
- **TestMathematicalEdgeCase_FractionalCoefficients**: Test với hệ số phân số

#### Input Validation Edge Cases
- **TestInputValidation_ScientificNotation**: Test ký hiệu khoa học
- **TestInputValidation_WhitespaceHandling**: Test xử lý khoảng trắng
- **TestInputValidation_InvalidFormats**: Test định dạng không hợp lệ

## Cách chạy Tests

### Trong Visual Studio:
1. Mở Test Explorer (Test > Test Explorer)
2. Build solution
3. Chạy tất cả tests hoặc chạy từng test riêng lẻ

### Trong Command Line:
```bash
# Build solution
msbuild P_TB2.sln

# Run tests using MSTest
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" UnitTestProject2\bin\Debug\UnitTestProject2.dll
```

## Các trường hợp test được bao phủ

### 1. Phương trình bậc 2 chuẩn
- 2 nghiệm phân biệt: x² - 5x + 6 = 0 → x₁ = 3, x₂ = 2
- 1 nghiệm kép: x² - 4x + 4 = 0 → x = 2
- Vô nghiệm: x² + 1 = 0

### 2. Phương trình đặc biệt
- Phương trình bậc 1: 2x - 4 = 0 → x = 2
- Phương trình hằng: 0 = 0 → vô số nghiệm
- Phương trình hằng: 5 = 0 → vô nghiệm

### 3. Input validation
- Số nguyên: "123", "-123"
- Số thập phân: "123.456", "-123.456"
- Số khoa học: "1e10", "-1e-10"
- Input không hợp lệ: "abc", "", null

### 4. Edge cases
- Số rất lớn và rất nhỏ
- Hệ số âm
- NaN, Infinity
- Giá trị biên

## Kết quả mong đợi

Tất cả tests phải pass với các tiêu chí:
- **Performance**: < 1000ms cho 1000 lần thực thi
- **Accuracy**: Kết quả chính xác đến 2 chữ số thập phân
- **Robustness**: Không crash với input bất thường
- **Coverage**: Bao phủ tất cả các nhánh code

## Troubleshooting

### Lỗi thường gặp:
1. **Reference not found**: Đảm bảo project reference đã được thêm đúng
2. **Build errors**: Kiểm tra target framework version
3. **Test failures**: Kiểm tra logic test và expected values

### Debug tests:
- Sử dụng breakpoints trong test methods
- Kiểm tra Test Output window
- Xem chi tiết lỗi trong Test Explorer

## Maintenance

### Thêm test mới:
1. Tạo test method với attribute `[TestMethod]`
2. Đặt tên theo format: `Test[Component]_[Scenario]`
3. Sử dụng Arrange-Act-Assert pattern
4. Thêm vào region phù hợp

### Cập nhật test:
- Khi thay đổi logic trong main project
- Khi thêm tính năng mới
- Khi sửa bug

## Tài liệu tham khảo

- [MSTest Documentation](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)
- [Unit Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
- [Test Driven Development](https://en.wikipedia.org/wiki/Test-driven_development)


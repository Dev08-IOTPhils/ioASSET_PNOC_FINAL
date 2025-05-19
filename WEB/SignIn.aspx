<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>IOT - ioASSET | Log in</title>

    <link rel="shortcut icon" href="<%= Page.ResolveClientUrl("~/Image/IOT/iot.ico") %>" />

    <link href="AdminLTE-2.4.7/bower_components/select2/dist/css/select2.min.css" rel="stylesheet" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="AdminLTE-2.4.7/bower_components/bootstrap/dist/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="AdminLTE-2.4.7/bower_components/font-awesome/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="AdminLTE-2.4.7/bower_components/Ionicons/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="AdminLTE-2.4.7/dist/css/AdminLTE.min.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="AdminLTE-2.4.7/plugins/iCheck/square/blue.css">

    <script>
        $('.select2').select2();
    </script>
    <script src="../AdminLTE-2.4.7/bower_components/select2/dist/js/select2.full.min.js"></script>
    <script src="../AdminLTE-2.4.7/jssor.slider.min.js"></script>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
</head>
<body class="hold-transition login-page ">
    <div class="login-box-carousel  col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <script src="../bootstrap/js/jssor.slider.min.js"></script>
        <script type="text/javascript">
            jssor_1_slider_init = function () {

                var jssor_1_SlideshowTransitions = [
                  { $Duration: 500, $Delay: 30, $Cols: 8, $Rows: 4, $Clip: 15, $SlideOut: true, $Formation: $JssorSlideshowFormations$.$FormationStraightStairs, $Assembly: 2049, $Easing: $Jease$.$OutQuad },
                  { $Duration: 500, $Delay: 80, $Cols: 8, $Rows: 4, $Clip: 15, $SlideOut: true, $Easing: $Jease$.$OutQuad },
                  //{ $Duration: 1000, x: -0.2, $Delay: 40, $Cols: 12, $SlideOut: true, $Formation: $JssorSlideshowFormations$.$FormationStraight, $Assembly: 260, $Easing: { $Left: $Jease$.$InOutExpo, $Opacity: $Jease$.$InOutQuad }, $Opacity: 2, $Outside: true, $Round: { $Top: 0.5 } },
                  //{ $Duration: 2000, y: -1, $Delay: 60, $Cols: 15, $SlideOut: true, $Formation: $JssorSlideshowFormations$.$FormationStraight, $Easing: $Jease$.$OutJump, $Round: { $Top: 1.5 } },
                  //{ $Duration: 1200, x: 0.2, y: -0.1, $Delay: 20, $Cols: 8, $Rows: 4, $Clip: 15, $During: { $Left: [0.3, 0.7], $Top: [0.3, 0.7] }, $Formation: $JssorSlideshowFormations$.$FormationStraightStairs, $Assembly: 260, $Easing: { $Left: $Jease$.$InWave, $Top: $Jease$.$InWave, $Clip: $Jease$.$OutQuad }, $Round: { $Left: 1.3, $Top: 2.5 } },


                  //{ $Duration: 500, $Delay: 80, $Cols: 8, $Rows: 4, $Clip: 15, $SlideOut: true, $Easing: $Jease$.$OutQuad },
                  //{ $Duration: 1200, y: 0.3, $Cols: 2, $During: { $Top: [0.3, 0.7] }, $ChessMode: { $Column: 12 }, $Easing: { $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
                  //{ $Duration: 1000, x: -1, y: 2, $Rows: 2, $Zoom: 11, $Rotate: 1, $SlideOut: true, $Assembly: 2049, $ChessMode: { $Row: 15 }, $Easing: { $Left: $Jease$.$InExpo, $Top: $Jease$.$InExpo, $Zoom: $Jease$.$InExpo, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InExpo }, $Opacity: 2, $Round: { $Rotate: 0.85 } },
                  //{ $Duration: 1200, x: 4, $Cols: 2, $Zoom: 11, $SlideOut: true, $Assembly: 2049, $ChessMode: { $Column: 15 }, $Easing: { $Left: $Jease$.$InExpo, $Zoom: $Jease$.$InExpo, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
                  //{ $Duration: 1000, x: 4, y: -4, $Zoom: 11, $Rotate: 1, $SlideOut: true, $Easing: { $Left: $Jease$.$InExpo, $Top: $Jease$.$InExpo, $Zoom: $Jease$.$InExpo, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InExpo }, $Opacity: 2, $Round: { $Rotate: 0.8 } },
                  //{ $Duration: 1500, x: 0.3, y: -0.3, $Delay: 80, $Cols: 8, $Rows: 4, $Clip: 15, $During: { $Left: [0.3, 0.7], $Top: [0.3, 0.7] }, $Easing: { $Left: $Jease$.$InJump, $Top: $Jease$.$InJump, $Clip: $Jease$.$OutQuad }, $Round: { $Left: 0.8, $Top: 2.5 } },
                  //{ $Duration: 1000, x: -3, y: 1, $Rows: 2, $Zoom: 11, $Rotate: 1, $SlideOut: true, $Assembly: 2049, $ChessMode: { $Row: 28 }, $Easing: { $Left: $Jease$.$InExpo, $Top: $Jease$.$InExpo, $Zoom: $Jease$.$InExpo, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InExpo }, $Opacity: 2, $Round: { $Rotate: 0.7 } },
                  //{ $Duration: 1000, x: 0.5, y: 0.5, $Zoom: 1, $Rotate: 1, $SlideOut: true, $Easing: { $Left: $Jease$.$InCubic, $Top: $Jease$.$InCubic, $Zoom: $Jease$.$InCubic, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InCubic }, $Opacity: 2, $Round: { $Rotate: 0.5 } },
                  //{ $Duration: 1200, x: -0.6, y: -0.6, $Zoom: 1, $Rotate: 1, $During: { $Left: [0.2, 0.8], $Top: [0.2, 0.8], $Zoom: [0.2, 0.8], $Rotate: [0.2, 0.8] }, $Easing: $Jease$.$Swing, $Opacity: 2, $Round: { $Rotate: 0.5 } },
                  //{ $Duration: 1500, y: -0.5, $Delay: 60, $Cols: 15, $SlideOut: true, $Formation: $JssorSlideshowFormations$.$FormationCircle, $Easing: $Jease$.$InWave, $Round: { $Top: 1.5 } },
                  //{ $Duration: 500, $Delay: 30, $Cols: 8, $Rows: 4, $Clip: 15, $Formation: $JssorSlideshowFormations$.$FormationStraightStairs, $Assembly: 2050, $Easing: $Jease$.$InQuad },
                  //{ $Duration: 1200, $Delay: 20, $Clip: 3, $SlideOut: true, $Assembly: 260, $Easing: { $Clip: $Jease$.$OutCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 }
                ];

                var jssor_1_SlideoTransitions = [
                  [{ b: -1, d: 1, o: -1 }, { b: 0, d: 1200, y: 300, o: 1, e: { y: 24, o: 6 } }, { b: 5600, d: 800, y: -200, o: -1, e: { y: 5 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 400, d: 800, x: 200, o: 1, e: { x: 27, o: 6 } }, { b: 5600, d: 800, x: -200, o: -1, e: { x: 5 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 400, d: 800, x: -200, o: 1, e: { x: 27, o: 6 } }, { b: 5600, d: 800, x: 200, o: -1, e: { x: 5 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 1600, d: 600, x: 200, y: -230, o: 1, e: { x: 3, y: 3 } }, { b: 5600, d: 800, o: -1 }],
                  [{ b: 4600, d: 960, x: -204, e: { x: 1 } }],
                  [{ b: -1, d: 1, sX: -1, sY: -1 }, { b: 3400, d: 400, sX: 1, sY: 1 }, { b: 3800, d: 300, o: -1, sX: 0.1, sY: 0.1 }],
                  [{ b: -1, d: 1, sX: -1, sY: -1 }, { b: 3520, d: 400, sX: 1, sY: 1 }, { b: 3920, d: 300, o: -1, sX: 0.1, sY: 0.1 }],
                  [{ b: -1, d: 1, o: -1 }, { b: 2200, d: 1200, x: -135, y: -24, o: 1, e: { x: 7, y: 7 } }, { b: 4600, d: 640, x: -130, e: { x: 1 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 4600, d: 240, x: -75, o: 1 }, { b: 4840, d: 480, x: -150 }, { b: 5320, d: 240, x: -75, o: -1 }],
                  [{ b: 2800, d: 600, y: 70, sX: -0.5, sY: -0.5, e: { y: 5 } }, { b: 6000, d: 600, y: 50, r: -10 }, { b: 7000, d: 400, o: -1, rX: 10, rY: -10 }],
                  [{ b: 0, d: 600, x: -742, sX: 4, sY: 4, e: { x: 6 } }, { b: 900, d: 600, sX: -4, sY: -4 }],
                  [{ b: -1, d: 1, o: -1 }, { b: 400, d: 500, o: 1, e: { o: 5 } }],
                  [{ b: -1, d: 1, o: -1, r: -180 }, { b: 1500, d: 500, o: 1, r: 180, e: { r: 27 } }],
                  [{ b: -1, d: 1, o: -1, r: 180 }, { b: 2000, d: 500, o: 1, r: -180, e: { r: 27 } }],
                  [{ b: 2800, d: 600, y: -270, e: { y: 6 } }],
                  [{ b: 6000, d: 600, y: -100, r: -10, e: { y: 6 } }, { b: 7000, d: 400, o: -1, rX: -10, rY: 10 }],
                  [{ b: -1, d: 1, sX: -1, sY: -1 }, { b: 3400, d: 400, sX: 1.33, sY: 1.33, e: { sX: 7, sY: 7 } }, { b: 3800, d: 200, sX: -0.33, sY: -0.33, e: { sX: 16, sY: 16 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 3400, d: 600, o: 1 }, { b: 4000, d: 1000, r: 360, e: { r: 1 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 3400, d: 600, y: -70, o: 1, e: { y: 27 } }],
                  [{ b: -1, d: 1, sX: -1, sY: -1 }, { b: 3700, d: 400, sX: 1.33, sY: 1.33, e: { sX: 7, sY: 7 } }, { b: 4100, d: 200, sX: -0.33, sY: -0.33, e: { sX: 16, sY: 16 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 3700, d: 600, o: 1 }, { b: 4300, d: 1000, r: 360 }],
                  [{ b: -1, d: 1, o: -1 }, { b: 3700, d: 600, x: -150, o: 1, e: { x: 27 } }],
                  [{ b: -1, d: 1, sX: -1, sY: -1 }, { b: 4000, d: 400, sX: 1.33, sY: 1.33, e: { sX: 7, sY: 7 } }, { b: 4400, d: 200, sX: -0.33, sY: -0.33, e: { sX: 16, sY: 16 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 4000, d: 600, o: 1 }, { b: 4600, d: 1000, r: 360 }],
                  [{ b: -1, d: 1, o: -1 }, { b: 4000, d: 600, x: 150, o: 1, e: { x: 27 } }],
                  [{ b: 9300, d: 600, o: -1, r: 540, sX: -0.5, sY: -0.5, e: { r: 5 } }],
                  [{ b: -1, d: 1, o: -1, sX: 2, sY: 2 }, { b: 6880, d: 20, o: 1 }, { b: 6900, d: 300, sX: -2.08, sY: -2.08, e: { sX: 27, sY: 27 } }, { b: 7200, d: 240, sX: 0.08, sY: 0.08 }],
                  [{ b: -1, d: 1, o: -1, sX: 5, sY: 5 }, { b: 6300, d: 600, o: 1, sX: -5, sY: -5 }],
                  [{ b: -1, d: 1, o: -1 }, { b: 7200, d: 440, o: 1 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 7420, d: 20, o: 1 }, { b: 7440, d: 200, r: 180, sX: 0.4, sY: 0.4 }, { b: 7640, d: 200, r: 180, sX: 0.5, sY: 0.5 }],
                  [{ b: -1, d: 1, o: -1, r: -60, sX: -0.9, sY: -0.9 }, { b: 7620, d: 20, o: 1 }, { b: 7640, d: 300, r: 60, sX: 1.1, sY: 1.1 }, { b: 7940, d: 160, sX: -0.2, sY: -0.2 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 7920, d: 20, o: 1 }, { b: 7940, d: 300, sX: 1.4, sY: 1.4 }, { b: 8240, d: 160, sX: -0.5, sY: -0.5 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 7620, d: 20, o: 1 }, { b: 7640, d: 200, r: 180, sX: 0.4, sY: 0.4 }, { b: 7840, d: 200, r: 180, sX: 0.5, sY: 0.5 }],
                  [{ b: -1, d: 1, o: -1, r: -60, sX: -0.9, sY: -0.9 }, { b: 7820, d: 20, o: 1 }, { b: 7840, d: 300, r: 60, sX: 1.1, sY: 1.1 }, { b: 8140, d: 160, sX: -0.2, sY: -0.2 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 8120, d: 20, o: 1 }, { b: 8140, d: 300, sX: 1.4, sY: 1.4 }, { b: 8440, d: 160, sX: -0.5, sY: -0.5 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 7820, d: 20, o: 1 }, { b: 7840, d: 200, r: 180, sX: 0.4, sY: 0.4 }, { b: 8040, d: 200, r: 180, sX: 0.5, sY: 0.5 }],
                  [{ b: -1, d: 1, o: -1, r: -60, sX: -0.9, sY: -0.9 }, { b: 8020, d: 20, o: 1 }, { b: 8040, d: 300, r: 60, sX: 1.1, sY: 1.1 }, { b: 8340, d: 160, sX: -0.2, sY: -0.2 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 8320, d: 20, o: 1 }, { b: 8340, d: 300, sX: 1.4, sY: 1.4 }, { b: 8640, d: 160, sX: -0.5, sY: -0.5 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 8020, d: 20, o: 1 }, { b: 8040, d: 200, r: 180, sX: 0.4, sY: 0.4 }, { b: 8240, d: 200, r: 180, sX: 0.5, sY: 0.5 }],
                  [{ b: -1, d: 1, o: -1, r: -60, sX: -0.9, sY: -0.9 }, { b: 8220, d: 20, o: 1 }, { b: 8240, d: 300, r: 60, sX: 1.1, sY: 1.1 }, { b: 8540, d: 160, sX: -0.2, sY: -0.2 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 8520, d: 20, o: 1 }, { b: 8540, d: 300, sX: 1.4, sY: 1.4 }, { b: 8840, d: 160, sX: -0.5, sY: -0.5 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 8220, d: 20, o: 1 }, { b: 8240, d: 200, r: 180, sX: 0.4, sY: 0.4 }, { b: 8440, d: 200, r: 180, sX: 0.5, sY: 0.5 }],
                  [{ b: -1, d: 1, o: -1, r: -60, sX: -0.9, sY: -0.9 }, { b: 8420, d: 20, o: 1 }, { b: 8440, d: 300, r: 60, sX: 1.1, sY: 1.1 }, { b: 8740, d: 160, sX: -0.2, sY: -0.2 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 8720, d: 20, o: 1 }, { b: 8740, d: 300, sX: 1.4, sY: 1.4 }, { b: 9040, d: 160, sX: -0.5, sY: -0.5 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 8220, d: 20, o: 1 }, { b: 8240, d: 200, r: 180, sX: 0.4, sY: 0.4 }, { b: 8440, d: 200, r: 180, sX: 0.5, sY: 0.5 }],
                  [{ b: -1, d: 1, o: -1, r: -60, sX: -0.9, sY: -0.9 }, { b: 8420, d: 20, o: 1 }, { b: 8440, d: 300, r: 60, sX: 1.1, sY: 1.1 }, { b: 8740, d: 160, sX: -0.2, sY: -0.2 }],
                  [{ b: -1, d: 1, o: -1, sX: -0.9, sY: -0.9 }, { b: 8720, d: 20, o: 1 }, { b: 8740, d: 300, sX: 1.4, sY: 1.4 }, { b: 9040, d: 160, sX: -0.5, sY: -0.5 }],
                  [{ b: -1, d: 1, sX: -0.2, sY: -0.2 }, { b: 0, d: 400, y: 330, e: { y: 1 } }, { b: 900, d: 400, y: 50, rX: 80, e: { y: 1, rX: 1 } }, { b: 20700, d: 1000, y: -95, rX: -80, sX: 0.2, sY: 0.2, e: { y: 16, rX: 16, sX: 16, sY: 16 } }, { b: 22700, d: 900, y: 25, o: -1, rX: 60 }],
                  [{ b: -1, d: 1, o: -0.5 }, { b: 900, d: 400, o: -0.5 }],
                  [{ b: -1, d: 1, sX: -0.2, sY: -0.2 }, { b: 1700, d: 400, y: 310, e: { y: 1 } }, { b: 2600, d: 400, y: 50, rX: 80, e: { y: 1, rX: 1 } }, { b: 19700, d: 1000, y: 20, e: { y: 1 } }, { b: 20700, d: 1000, y: -95, rX: -80, sX: 0.2, sY: 0.2, e: { y: 16, rX: 16, sX: 16, sY: 16 } }, { b: 22700, d: 900, y: 25, o: -1, rX: 60 }],
                  [{ b: 19700, d: 1000, o: -1 }],
                  [{ b: -1, d: 1, o: -0.5 }, { b: 2600, d: 400, o: -0.5 }],
                  [{ b: -1, d: 1, sX: -0.2, sY: -0.2 }, { b: 3400, d: 400, y: 290, e: { y: 1 } }, { b: 4800, d: 400, y: 50, rX: 80, e: { y: 1, rX: 1 } }, { b: 19700, d: 1000, y: 40, e: { y: 1 } }, { b: 20700, d: 1000, y: -95, rX: -80, sX: 0.2, sY: 0.2, e: { y: 16, rX: 16, sX: 16, sY: 16 } }, { b: 22700, d: 900, y: 25, o: -1, rX: 60 }],
                  [{ b: 19700, d: 1000, o: -1 }],
                  [{ b: -1, d: 1, c: { t: -280 } }, { b: 3800, d: 100, c: { t: 50.40 }, e: { c: { t: 32 } } }, { b: 3900, d: 100, c: { t: 33.60 }, e: { c: { t: 32 } } }, { b: 4000, d: 100, c: { t: 30.80 }, e: { c: { t: 32 } } }, { b: 4100, d: 100, c: { t: 30.80 }, e: { c: { t: 32 } } }, { b: 4200, d: 100, c: { t: 33.60 }, e: { c: { t: 32 } } }, { b: 4300, d: 100, c: { t: 22.40 }, e: { c: { t: 32 } } }, { b: 4400, d: 100, c: { t: 28.00 }, e: { c: { t: 32 } } }, { b: 4500, d: 100, c: { t: 50.40 }, e: { c: { t: 32 } } }],
                  [{ b: -1, d: 1, o: -0.5 }, { b: 4800, d: 400, o: -0.5 }],
                  [{ b: -1, d: 1, sX: -0.2, sY: -0.2 }, { b: 5700, d: 400, y: 270, e: { y: 1 } }, { b: 6600, d: 400, y: 50, rX: 40, e: { y: 1, rX: 1 } }, { b: 14700, d: 500, rX: 40, e: { rX: 1 } }, { b: 19700, d: 1000, y: 60, e: { y: 1 } }, { b: 20700, d: 1000, y: -95, rX: -80, sX: 0.2, sY: 0.2, e: { y: 16, rX: 16, sX: 16, sY: 16 } }, { b: 22700, d: 900, y: 25, o: -1, rX: 60 }],
                  [{ b: 19700, d: 1000, o: -1 }],
                  [{ b: 6600, d: 400, o: -0.2 }, { b: 14700, d: 500, o: -0.8 }],
                  [{ b: -1, d: 1, o: -0.5 }, { b: 14700, d: 500, o: -0.5 }],
                  [{ b: -1, d: 1, sX: -0.2, sY: -0.2 }, { b: 7500, d: 400, y: 270, e: { y: 1 } }, { b: 8400, d: 400, y: 50, rX: 40, e: { y: 1, rX: 1 } }, { b: 14700, d: 500, rX: 40, e: { rX: 1 } }, { b: 19700, d: 1000, y: 60, e: { y: 1 } }, { b: 20700, d: 1000, y: -95, rX: -80, sX: 0.2, sY: 0.2, e: { y: 16, rX: 16, sX: 16, sY: 16 } }, { b: 22700, d: 900, y: 25, o: -1, rX: 60 }],
                  [{ b: 8400, d: 400, o: -1 }],
                  [{ b: -1, d: 1, o: -0.5 }, { b: 8400, d: 400, o: -0.5 }],
                  [{ b: -1, d: 1, sX: -0.2, sY: -0.2 }, { b: 9300, d: 400, y: 270, e: { y: 1 } }, { b: 10200, d: 400, y: 50, rX: 40, e: { y: 1 } }, { b: 14700, d: 500, rX: 40, e: { rX: 1 } }, { b: 19700, d: 1000, y: 60, e: { y: 1 } }, { b: 20700, d: 1000, y: -95, rX: -80, sX: 0.2, sY: 0.2, e: { y: 16, rX: 16, sX: 16, sY: 16 } }, { b: 22700, d: 900, y: 25, o: -1, rX: 60 }],
                  [{ b: 10200, d: 400, o: -1 }],
                  [{ b: -1, d: 1, o: -0.5 }, { b: 10200, d: 400, o: -0.5 }],
                  [{ b: -1, d: 1, sX: -0.2, sY: -0.2 }, { b: 11100, d: 400, y: 270, e: { y: 1 } }, { b: 12000, d: 400, y: 50, rX: 40, e: { y: 1, rX: 1 } }, { b: 14700, d: 500, rX: 40, e: { rX: 1 } }, { b: 19700, d: 1000, y: 60, e: { y: 1 } }, { b: 20700, d: 1000, y: -95, rX: -80, sX: 0.2, sY: 0.2, e: { y: 16, rX: 16, sX: 16, sY: 16 } }, { b: 22700, d: 900, y: 25, o: -1, rX: 60 }],
                  [{ b: 12000, d: 400, o: -1 }],
                  [{ b: -1, d: 1, o: -0.5 }, { b: 12000, d: 400, o: -0.5 }],
                  [{ b: -1, d: 1, sX: -0.2, sY: -0.2 }, { b: 12900, d: 400, y: 270, e: { y: 1 } }, { b: 13800, d: 400, y: 50, rX: 40, e: { y: 1, rX: 1 } }, { b: 14700, d: 500, rX: 40, e: { rX: 1 } }, { b: 19700, d: 1000, y: 60, e: { y: 1 } }, { b: 20700, d: 1000, y: -95, rX: -80, sX: 0.2, sY: 0.2, e: { y: 16, rX: 16, sX: 16, sY: 16 } }, { b: 22700, d: 900, y: 25, o: -1, rX: 60 }],
                  [{ b: 13800, d: 400, o: -1 }],
                  [{ b: -1, d: 1, o: -0.5 }, { b: 13800, d: 400, o: -0.5 }],
                  [{ b: -1, d: 1, sX: -0.2, sY: -0.2 }, { b: 15700, d: 400, y: 270, e: { y: 1 } }, { b: 18800, d: 400, y: 30, rX: 80, e: { y: 1, rX: 1 } }, { b: 19700, d: 1000, y: 80, e: { y: 1 } }, { b: 20700, d: 1000, y: -95, rX: -80, sX: 0.2, sY: 0.2, e: { y: 16, rX: 16, sX: 16, sY: 16 } }, { b: 22700, d: 900, y: 25, o: -1, rX: 60 }],
                  [{ b: 19700, d: 1000, o: -1 }],
                  [{ b: -1, d: 1, o: -0.5 }, { b: 18800, d: 400, o: -0.5 }],
                  [{ b: -1, d: 1, o: -1 }, { b: 16100, d: 300, o: 1 }, { b: 16400, d: 500, x: -238, e: { x: 6 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 16100, d: 300, o: 1 }, { b: 16400, d: 500, x: 238, e: { x: 6 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 16700, d: 400, y: 200, o: 1, e: { y: 2, o: 6 } }, { b: 17100, d: 300, y: -28, e: { y: 3 } }, { b: 17400, d: 300, y: 28, e: { y: 2 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 16900, d: 400, y: 200, o: 1, e: { y: 2, o: 6 } }, { b: 17300, d: 300, y: -28, e: { y: 3 } }, { b: 17600, d: 300, y: 28, e: { y: 2 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 17100, d: 400, y: 200, o: 1, e: { y: 2, o: 6 } }, { b: 17500, d: 300, y: -28, e: { y: 3 } }, { b: 17800, d: 300, y: 28, e: { y: 2 } }],
                  [{ b: -1, d: 1, o: -1 }, { b: 17300, d: 400, y: 200, o: 1, e: { y: 2, o: 6 } }, { b: 17700, d: 300, y: -28, e: { y: 3 } }, { b: 18000, d: 300, y: 28, e: { y: 2 } }]
                ];

                var jssor_1_options = {
                    $AutoPlay: 1,
                    $SlideDuration: 1000,
                    $SlideEasing: $Jease$.$OutQuint,
                    $SlideshowOptions: {
                        $Class: $JssorSlideshowRunner$,
                        $Transitions: jssor_1_SlideshowTransitions,
                        $TransitionsOrder: 1
                    },
                    //$CaptionSliderOptions: {
                    //  $Class: $JssorCaptionSlideo$,
                    //  $Transitions: jssor_1_SlideoTransitions,
                    //  $Breaks: [
                    //    [{d:2000,b:5600,t:2}],
                    //    [{d:2000,b:9300,t:2}],
                    //    [{d:2000,b:22700}]
                    //  ]
                    //},
                    $ArrowNavigatorOptions: {
                        $Class: $JssorArrowNavigator$
                    },
                    $BulletNavigatorOptions: {
                        $Class: $JssorBulletNavigator$
                    }
                };

                var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

                /*#region responsive code begin*/

                var MAX_WIDTH = 700;

                function ScaleSlider() {
                    var containerElement = jssor_1_slider.$Elmt.parentNode;
                    var containerWidth = containerElement.clientWidth;

                    if (containerWidth) {

                        var expectedWidth = Math.min(MAX_WIDTH || containerWidth, containerWidth);

                        jssor_1_slider.$ScaleWidth(expectedWidth);
                    }
                    else {
                        window.setTimeout(ScaleSlider, 30);
                    }
                }

                ScaleSlider();

                $Jssor$.$AddEvent(window, "load", ScaleSlider);
                $Jssor$.$AddEvent(window, "resize", ScaleSlider);
                $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);


                /*
                    parameters to scale jssor slider to fill a container
    
                    MAX_WIDTH
                        prevent slider from scaling too wide
                    MAX_HEIGHT
                        prevent slider from scaling too high, default value is original height
                    MAX_BLEEDING
                        prevent slider from bleeding outside too much, default value is 1
                        0: contain mode, allow up to 0% to bleed outside, the slider will be all inside container
                        1: cover mode, allow up to 100% to bleed outside, the slider will cover full area of container
                        0.1: flex mode, allow up to 10% to bleed outside, this is better way to make full window slider, especially for mobile devices
                */

                //var MAX_WIDTH = 10000;
                //var MAX_HEIGHT = 10000;
                //var MAX_BLEEDING = 1;

                //function ScaleSlider() {
                //    var containerElement = jssor_1_slider.$Elmt.parentNode;
                //    var containerWidth = containerElement.clientWidth;

                //    if (containerWidth) {
                //        var originalWidth = jssor_1_slider.$OriginalWidth();
                //        var originalHeight = jssor_1_slider.$OriginalHeight();

                //        var containerHeight = containerElement.clientHeight || originalHeight;

                //        var expectedWidth = Math.min(MAX_WIDTH || containerWidth, containerWidth);
                //        var expectedHeight = Math.min(MAX_HEIGHT || containerHeight, containerHeight);

                //        //constrain bullets, arrows inside slider area, it's optional, remove it if not necessary
                //        //if (MAX_BLEEDING >= 0 && MAX_BLEEDING < 1) {
                //        //    var widthRatio = expectedWidth / originalWidth;
                //        //    var heightRatio = expectedHeight / originalHeight;
                //        //    var maxScaleRatio = Math.max(widthRatio, heightRatio);
                //        //    var minScaleRatio = Math.min(widthRatio, heightRatio);

                //        //    maxScaleRatio = Math.min(maxScaleRatio / minScaleRatio, 1 / (1 - MAX_BLEEDING)) * minScaleRatio;
                //        //    expectedWidth = Math.min(expectedWidth, originalWidth * maxScaleRatio);
                //        //    expectedHeight = Math.min(expectedHeight, originalHeight * maxScaleRatio);
                //        //}

                //        //scale the slider to expected size
                //        jssor_1_slider.$ScaleSize(expectedWidth, expectedHeight, MAX_BLEEDING);

                //        //position slider at center in vertical orientation
                //        jssor_1_slider.$Elmt.style.top = ((containerHeight - expectedHeight) / 2) + "px";

                //        //position slider at center in horizontal orientation
                //        jssor_1_slider.$Elmt.style.left = ((containerWidth - expectedWidth) / 2) + "px";
                //    }
                //    else {
                //        window.setTimeout(ScaleSlider, 30);
                //    }
                //}

                function OnOrientationChange() {
                    ScaleSlider();
                    window.setTimeout(ScaleSlider, 800);
                }

                ScaleSlider();

                $(window).bind("load", ScaleSlider);
                $(window).bind("resize", ScaleSlider);
                $(window).bind("orientationchange", OnOrientationChange);

                /*#endregion responsive code end*/
            };
        </script>
        <style>
            .thumbImage {
                max-width: 600px;
                max-height: 450px;
            }
            /* jssor slider loading skin spin css */
            .jssorl-009-spin img {
                animation-name: jssorl-009-spin;
                animation-duration: 1.6s;
                animation-iteration-count: infinite;
                animation-timing-function: linear;
            }

            @keyframes jssorl-009-spin {
                from {
                    transform: rotate(0deg);
                }

                to {
                    transform: rotate(360deg);
                }
            }


            .jssorb101 .i {
                position: absolute;
                cursor: pointer;
            }

                .jssorb101 .i .ci {
                    fill: #000;
                }

                .jssorb101 .i .co {
                    fill: #fff;
                    opacity: .3;
                }

                .jssorb101 .i:hover .co {
                    fill: #ff9933;
                    opacity: 1;
                }

                .jssorb101 .i:hover .ci {
                    fill: #fff;
                }

            .jssorb101 .iav .ci {
                fill: #fff;
                stroke-width: 0;
            }

            .jssorb101 .iav .co {
                fill: #46d1d3;
                opacity: 1;
            }

            .jssorb101 .i.idn {
                opacity: .3;
            }

            .jssora051 {
                display: block;
                position: absolute;
                cursor: pointer;
            }

                .jssora051 .a {
                    fill: none;
                    stroke: #fff;
                    stroke-width: 360;
                    stroke-miterlimit: 10;
                }

                .jssora051:hover {
                    opacity: .8;
                }

                .jssora051.jssora051dn {
                    opacity: .5;
                }

                .jssora051.jssora051ds {
                    opacity: .3;
                    pointer-events: none;
                }
        </style>
        <br />
        <br />
        <div id="jssor_1" style="position: relative; margin: 0 auto; top: 0px; left: 0px; width: 700px; height: 500px; overflow: hidden; visibility: hidden;">
            <!-- Loading Screen -->
            <div data-u="loading" class="jssorl-009-spin" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; text-align: center; background-color: rgba(0,0,0,0.7);">
                <img style="margin-top: -19px; position: relative; top: 50%; width: 38px; height: 38px;" src="../svg/loading/static-svg/spin.svg" />
            </div>
            <div id="slideshowid" runat="server" data-u="slides" style="cursor: default; position: relative; top: 0px; left: 0px; width: 700px; height: 500px; overflow: hidden;">
                <%-- <div data-b="0">
                <img data-u="image" src="../img/present/01.jpg" />
            </div>
            <div data-b="1" data-p="170.00">
                <img data-u="image" src="../img/present/02.jpg" />
            </div>
            <div data-b="2" data-p="816.0">
                <img data-u="image" src="../img/present/03.jpg" />                
            </div>--%>
            </div>
            <!-- Bullet Navigator -->
            <div data-u="navigator" class="jssorb101" style="position: absolute; bottom: 12px; right: 12px;" data-autocenter="1" data-scale="0.5" data-scale-bottom="0.75">
                <div data-u="prototype" class="i" style="width: 16px; height: 16px;">
                    <svg viewBox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                        <circle class="co" cx="8000" cy="8000" r="5000"></circle>
                        <circle class="ci" cx="8000" cy="8000" r="3000"></circle>
                    </svg>
                </div>
            </div>
            <!-- Arrow Navigator -->
            <div data-u="arrowleft" class="jssora051" style="width: 55px; height: 55px; top: 0px; left: 25px;" data-autocenter="2" data-scale="0.75" data-scale-left="0.75">
                <svg viewBox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                    <polyline class="a" points="11040,1920 4960,8000 11040,14080 "></polyline>
                </svg>
            </div>
            <div data-u="arrowright" class="jssora051" style="width: 55px; height: 55px; top: 0px; right: 25px;" data-autocenter="2" data-scale="0.75" data-scale-right="0.75">
                <svg viewBox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                    <polyline class="a" points="4960,1920 11040,8000 4960,14080 "></polyline>
                </svg>
            </div>
        </div>
        <script type="text/javascript">jssor_1_slider_init();</script>
    </div>
    <div class="login-box-credentials  col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="login-logo" runat="server" id="LoginImage">
            <%--<a><img src="Image/LogoAndName.png" style="width:300px; height:130px;" /></a>--%>
        </div>
        <!-- /.login-logo -->
        <div class="login-box-body">
            <form id="form1" runat="server">
                <div>

                    <asp:Label ID="lbaelDB" runat="server" Text="Select Database" />
                    <div class="form-group">
                        <label class="sr-only">Database</label>
                        <asp:DropDownList ID="ddlDatabase" class="form-control select2" runat="server" Style="width: 100%; height: 35px"></asp:DropDownList>
                    </div>
                    <asp:Label ID="lblError" runat="server" Text="Enter Username & Password" />
                    <div class="form-group">
                        <label class="sr-only">Username</label>
                        <asp:TextBox ID="txtUserId" runat="server" required="true" placeholder="Username..." class="form-control" autofocus></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label class="sr-only">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" placeholder="Password..." class="form-control" TextMode="Password"></asp:TextBox>
                    </div>

                    <div align="center" id="login">
                        <asp:Button ID="btnLogin" runat="server" type="submit" class="btn btn-med btn-primary" Text="Sign in" OnClick="btnLogin_Click" Style="width: 100%"></asp:Button>
                    </div>
                    <hr />
                    <%--<p > Copyright © 2014-2018 Internet Of Things Phils. Inc. All rights reserved. <a href="http://www.iotphils.com">"Internet Of Things"</a>is a registered trademark of Internet Of Things Phils. Inc</p>--%>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <%-- <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" align="right"><img src="../Image/IOT/iot.png" width="100%"><h3 align="center">IOT PHILS</h3></div>--%>
                            <%--<div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" align="left">--%><h3 align="center">RFID - ioASSET</h3>

                            <%--</div>--%>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <center><h6>Powered by <a href="http://www.iotphils.com"><img src="Image/IOT/iot.png" width="8%"></a></h6></center>
                        </div>
                    </div>
                </div>

            </form>
        </div>
    </div>
</body>
</html>

  	 function needMap() {
		  	 	 var href = location.href;
		  	 	 return href.indexOf('map') != -1
						|| href.indexOf('mix3') != -1
						|| href.indexOf('mix5') != -1
						|| href.indexOf('dataRange') != -1;

		  	 }

		  	 var fileLocation = needMap() ? 'assets/js/echarts-map' : 'assets/js/echarts';
		  	 require.config({
		  	 	 paths: {
		  	 	 	 echarts: fileLocation,'echarts/assets/js/pie': fileLocation, 
					 'echarts/assets/js/map': fileLocation,
		  	 	 }
		  	 });

		  	 require(
						[
							'echarts','echarts/chart/pie',needMap() ? 'echarts/chart/map' : 'echarts'
						],
						 DrawCharts
			    );
		  	 function DrawCharts(ec) {
		  	 	FunDraw1(ec);
		  	 	 FunDraw2(ec);
		  
		  	 	
		  	 }
		  
		  	 	 function FunDraw2(ec) {
		  	 	 //--- 柱状图 ---
		  	 	     var myChart = ec.init(document.getElementById('ordersCharts'));
		  	 	 myChart.showLoading({
		  	 	 	 text: "加载中...请等待"
		  	 	 });
		  	 	 myChart.hideLoading();
		  	 	 myChart.setOption({
		  	 	 	 title: {
		  	 	 	 	 text: '本月每天订单数量统计（单）',
		  	 	 	 	 subtext: '数据来自后台统计'
		  	 	 	 },
		  	 	 	 tooltip: {
		  	 	 	 	 trigger: 'item',
		  	 	 	 	 axisPointer: {
		  	 	 	 	 	 type: 'shadow'
		  	 	 	 	 }
		  	 	 	 },
		  	 	 	 legend: {
		  	 	 	 	 data: [],
		  	 	 	 	 x: 'right',
		  	 	 	 },
		  	 	 	 toolbox: {
		  	 	 	 	 show: true, orient: 'vertical',
		  	 	 	 	 y: 'center',
		  	 	 	 	 feature: {
		  	 	 	 	 	 magicType: { show: true, type: ['line', 'bar'] },
		  	 	 	 	 	 restore: { show: true },
		  	 	 	 	 	 saveAsImage: { show: true }
		  	 	 	 	 }
		  	 	 	 },
		  	 	 	 calculable: true,
		  	 	 	 xAxis: { data: [], orient: 'vertical' },
		  	 	 	 yAxis: { type: 'value' },
		  	 	 	 series: []
		  	 	 });
		  	 	 // 通过Ajax获取数据
		  	 	 $.ajax({
		  	 	 	 type: "post",
		  	 	 	 async: false, //同步执行
		  	 	 	 url: "EnshineUnionHandler.ashx?ordersCharts=BarChart",
		  	 	 	 dataType: "json", //返回数据形式为json
		  	 	 	 success: function (result) {
		  	 	 	 	 if (result) {
		  	 	 	 	 	 //将返回的category和series对象赋值给options对象内的category和series
		  	 	 	 	 	 myChart._option.xAxis.data = result.category;
		  	 	 	 	 	 myChart._option.series = result.series;
		  	 	 	 	 	 myChart._option.legend.data = result.legend;
		  	 	 	 	 	 myChart.hideLoading();
		  	 	 	 	 	 myChart.setOption(myChart._option);
		  	 	 	 	 }
		  	 	 	 },
		  	 	 	 error: function (errorMsg) {
		  	 	 	 	 alert("每月订单数量统计数据请求失败。");
		  	 	 	 }
		  	 	 });

		  	 }


		   //饼状图
	            function FunDraw1(ec) {
	                var myChart = ec.init(document.getElementById('goodsCharts'));
	                var seriesPieData;
	                var legendData;
	                //判断饼状图坐标显示高度
	                var centerPie;
	                // 通过Ajax获取数据
	                $.ajax({
	                    type: "post",
	                    async: false, //同步执行
	                    url: "EnshineUnionHandler.ashx?goodsCharts=PieChart",
	                    dataType: "json", //返回数据形式为json
	                    success: function (result) {
	                        if (result) {
	                            seriesPieData = eval('(' + result.split('_')[0] + ')');
	                            legendData = eval('(' + result.split('_')[1] + ')');
	                            centerPie = legendData.length > 15 ? '70%' : '50%';
	                        }
	                    },
	                    error: function (errorMsg) {
	                        alert("当天销售商品统计分析请求失败。");
	                    }
	                });

	                myChart.setOption({
	                    title: {
	                        text: '每天销售商品统计分析',
	                        subtext: '数据来源于后台统计',
	                        x: 'right'
	                    },
	                    tooltip: {
	                        trigger: 'item',
	                        formatter: "{a} <br/>{b} : {c} ({d}%)"
	                    },
	                    legend: {
	                        orient: 'vertical',
	                        x: 'left',
	                        data: legendData
	                    },
	                    toolbox: {
	                        show: true,
	                        orient: 'vertical',
	                        y: 'center',
	                        feature: {
	                            mark: { show: true },
	                            restore: { show: true, },
	                            saveAsImage: { show: true }
	                        }
	                    },
	                    calculable: true,
	                    series: [
                            {
                                name: '每天销售商品名称及件数',
                                type: 'pie',
                                radius: '45%',
                                center: ['50%', centerPie],
                                data: seriesPieData
                            }
	                    ]
	                });
	            }
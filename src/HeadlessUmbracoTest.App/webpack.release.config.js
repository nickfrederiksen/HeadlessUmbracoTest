const glob = require("glob");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const htmlLoader = require("html-loader");
const tslintLoader = require("tslint-loader");
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const webpack = require('webpack'); //to access built-in plugins

module.exports = {
	mode: 'production',
	entry: {
		'./../dist/custom.min': glob.sync("./scripts/custom/**/*.ts", {
			ignore: ["./scripts/custom/**/*.d.ts"]
		}),
		'./../dist/site.min': "./Content/styles/site.scss"
	},
	output: {
		filename: '[name].js',
		libraryTarget: 'var',
		library: 'HeadlessUmbracoTest.App'
	},
	plugins: [
		new VueLoaderPlugin(),
		new MiniCssExtractPlugin({
			filename: "[name].css"
		})
	],
	resolve: {
		extensions: ['.ts', '.js', '.vue', '.css', '.scss'],
		alias: {
			vue: 'vue/dist/vue.esm.js'
		}
	},
	module: {
		rules: [
			{
				test: /\.vue$/,
				loader: 'vue-loader',
				options: {}
			},
			{
				test: /\.tsx?$/,
				use: [{
					loader: 'ts-loader',
					options: {
						onlyCompileBundledFiles: true,

						appendTsSuffixTo: [/\.vue$/],
					}
				}]
			},
			{
				test: /\.tsx?$/,
				enforce: 'pre',
				use: [
					{
						loader: 'tslint-loader',
						options: {
							configFile: "tslint.json",
							failOnHint: false
						}
					}
				]
			},
			{
				test: /\.(sass|scss|css)$/,
				use: [
					{
						loader: MiniCssExtractPlugin.loader
					},
					{
						loader: "css-loader",
						options: {
							modules: false,
							sourceMap: false
						}
					},
					{
						loader: "postcss-loader",
						options: {
							sourceMap: false,
							plugins: () => [
								require("autoprefixer")({
									browsers: ["> 1%", "last 2 versions"]
								})
							]
						}
					},
					{
						loader: "sass-loader",
						options: {
							sourceMap: false
						}
					}
				]
			}
		]
	}
};
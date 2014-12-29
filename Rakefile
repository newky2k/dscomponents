require "rake/clean"

CLEAN.include "*.xam"
CLEAN.include ".xpkg"

file ".xpkg/xamarin-component.exe" do
	puts "* Downloading xpkg..."
	mkdir "xpkg"
	sh "curl -L https://components.xamarin.com/submit/xpkg > xpkg.zip"
	sh "unzip -q -o xpkg.zip -d .xpkg"
	sh "rm xpkg.zip"
end

task :default => ".xpkg/xamarin-component.exe" do
	line = <<-END
	mono .xpkg/xamarin-component.exe package
		END
	puts "* Creating DSGridView Component"
	puts line.strip.gsub "\t\t", "\\\n    "
	sh line, :verbose => false
	puts "* Created DSGridView Component"
end

task :default => ".xpkg/xamarin-component.exe" do
	puts "* Creating DSGridView Component Trial"
	sh "mv component.yaml component-full.yaml"
	sh "mv component-trial.yaml component.yaml"
	line = <<-END
	mono .xpkg/xamarin-component.exe package
		END
	puts line.strip.gsub "\t\t", "\\\n    "
	sh line, :verbose => false
	sh "mv component.yaml component-trial.yaml"
	sh "mv component-full.yaml component.yaml"
	puts "* Created DSGridView Component"
end


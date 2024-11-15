const defaultConf = {
    "parallel": 1,
    "format": [
        "html:output/report.html",
        "json:output/report.json",
        "junit:output/junit.xml"
    ]

}
const ci = {
    "parallel" : 2,
    "format": [
        "html:output/report.html",
        "json:output/report.json",
        "junit:output/junit.xml"
    ]
}

export default defaultConf;
export { ci };
import time
import random

from selenium import webdriver
from selenium.webdriver.chrome.options import Options

options = Options()
options.add_argument("--no-sandbox")
options.add_argument("--start-maximized")
path = './chromedriver'
driver = webdriver.Chrome(options=options, executable_path=path)

port = "44304"
uri = "/User/LoginPage"
url = "https://localhost:" + port + uri
driver.get(url)

LOG_LOGIN_PATH = "//input[@name=\"login\"]"
LOG_PASSWORD_PATH = "//input[@name=\"password\"]"
LOG_BUTTON_PATH = "//input[@value=\"Войти\"]"

MAIN_LOGIN_PATH = "//*[@id=\"navbarCollapse\"]/p"

name = "Smith"
login = "smith"
password = "B12345"

try:

    time.sleep(1)

    driver.find_element_by_xpath(LOG_LOGIN_PATH).send_keys(login)
    driver.find_element_by_xpath(LOG_PASSWORD_PATH).send_keys(password)
    driver.find_element_by_xpath(LOG_BUTTON_PATH).click()

    time.sleep(1)

    actual_name = driver.find_element_by_xpath(MAIN_LOGIN_PATH).text[14:]

    if name == actual_name:
        print("CP-T1: All OK")
    else:
        print("CP-T1: Something not OK")

except ValueError:
    print("CP-T1: Something go wrong")

finally:
    driver.close()
